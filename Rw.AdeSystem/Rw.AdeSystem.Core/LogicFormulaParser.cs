using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Rw.AdeSystem.Core
{
    public static class LogicFormulaParser
    {

        public static List<string> GetConditions(string expression)
        {
            List<string> lv;
            List<Token> l;
            var x = Parse(expression, out l, out lv);
            x = SimplifyIf(x);
            x = AndOrReformTree(x);
            var val = GetFluentStrings(x).Select(i => i.Replace("&", ", ")).ToList();
            return val;

        }

        //metoda parsuje wyrazenie logiczne do drzewa
        public static BoolExpr Parse(string expr, out List<Token> literals, out List<string> literalValues)
        {
            expr = expr.Trim();
            expr = expr.Replace(" ", "");

            for (int i = 0; i < expr.Length; i++)
            {
                if (expr[i] == '!')
                {
                    if (expr[i + 1] != '(')
                    {
                        int j = i + 1;
                        while (j < expr.Length && expr[j] != '-' && expr[j] != '<' && expr[j] != '|' && expr[j] != '&')
                            j++;
                        expr = expr.Insert(i, "(");
                        expr = expr.Insert(j + 1, ")");
                    }
                }
                i++;
            }

            literals = new List<Token>();
            literalValues = new List<string>();
            var tokens = new List<Token>();
            var reader = new StringReader(expr);

            //Tokenize the expression
            Token t;
            do
            {
                t = new Token(reader);
                tokens.Add(t);
            } while (t.Type != Token.TokenType.ExprEnd);

            //Use a minimal version of the Shunting Yard algorithm to transform the token list to polish notation
            var polishNotation = TransformToPolishNotation(tokens);

            literals.AddRange(polishNotation.Where(i => i.Type == Token.TokenType.Literal).ToList());

            var enumerator = polishNotation.GetEnumerator();
            enumerator.MoveNext();
            var root = Make(ref enumerator);

            TraverseTree(root, null, literalValues);
            return root;
            //Eval the expression tree
            //Console.WriteLine(@"Eval: {0}", Eval(root));
        }

        //metoda usuwa implikacje i rownowaznosci
        public static BoolExpr SimplifyIf(BoolExpr tree)
        {
            var result = new BoolExpr(tree);

            _SimplifyIf(result);

            return result;

        }

        private static void _SimplifyIf(BoolExpr result)
        {
            if (result.IsLeaf())
            {
                return;
            }
            if (result.Op == BoolExpr.Bop.If)
            {
                var nowy = BoolExpr.CreateOr(result.Right, BoolExpr.CreateNot(result.Left));
                result.Left = nowy.Left;
                result.Right = nowy.Right;
                result.Op = nowy.Op;
                result.Lit = nowy.Lit;
            }
            else if (result.Op == BoolExpr.Bop.IfOnlyIf)
            {
                var nowy = BoolExpr.CreateAnd(BoolExpr.CreateIf(result.Left, result.Right), BoolExpr.CreateIf(result.Right, result.Left));
                result.Left = nowy.Left;
                result.Right = nowy.Right;
                result.Op = nowy.Op;
                result.Lit = nowy.Lit;
            }
            if (result.Left != null)
                _SimplifyIf(result.Left);
            if (result.Right != null)
                _SimplifyIf(result.Right);
        }

        //metoda zamienia wyrazenie(drzewo) w postac koniunkcji oddzielonych alternatywami np. (a&b)|(a&c)
        public static BoolExpr AndOrReformTree(BoolExpr tree)
        {
            var result = new BoolExpr(tree);
            var queue = new Queue<BoolExpr>();

            bool isChanged;
            do
            {
                queue.Enqueue(result);
                do
                {
                    isChanged = false;
                    var expr = queue.Dequeue();
                    if (expr.IsLeaf())
                        break;
                    if (expr.Op == BoolExpr.Bop.And)
                    {
                        if (expr.Left.Op == BoolExpr.Bop.Or)
                        {
                            BoolExpr nowy = BoolExpr.CreateOr(BoolExpr.CreateAnd(expr.Left.Right, expr.Right),
                                BoolExpr.CreateAnd(expr.Left.Left, expr.Right));
                            expr.Left = nowy.Left;
                            expr.Right = nowy.Right;
                            expr.Op = nowy.Op;
                            expr.Lit = nowy.Lit;

                            isChanged = true;
                            break;
                        }
                        if (expr.Right.Op == BoolExpr.Bop.Or)
                        {
                            BoolExpr nowy = BoolExpr.CreateOr(BoolExpr.CreateAnd(expr.Right.Right, expr.Right),
                                BoolExpr.CreateAnd(expr.Right.Left, expr.Right));
                            expr.Left = nowy.Left;
                            expr.Right = nowy.Right;
                            expr.Op = nowy.Op;
                            expr.Lit = nowy.Lit;
                            isChanged = true;
                           
                            break;
                        }
                    }
                    if (expr.Op == BoolExpr.Bop.Not)
                    {
                        if (expr.Left.Op == BoolExpr.Bop.And)
                        {
                            BoolExpr nowy = BoolExpr.CreateOr(BoolExpr.CreateNot(expr.Left.Right),
                                BoolExpr.CreateNot(expr.Left.Left));
                            expr.Left = nowy.Left;
                            expr.Right = nowy.Right;
                            expr.Op = nowy.Op;
                            expr.Lit = nowy.Lit;
                            isChanged = true;
                        
                            break;
                        }
                        if (expr.Left.Op == BoolExpr.Bop.Or)
                        {
                            BoolExpr nowy = BoolExpr.CreateAnd(BoolExpr.CreateNot(expr.Left.Right),
                                BoolExpr.CreateNot(expr.Left.Left));
                            expr.Left = nowy.Left;
                            expr.Right = nowy.Right;
                            expr.Op = nowy.Op;
                            expr.Lit = nowy.Lit;
                            isChanged = true;
                           
                            break;
                        }
                        if (expr.Left.Op == BoolExpr.Bop.Not)
                        {
                            var left = expr.Left.Left;
                            expr.Left = left.Left;
                            expr.Right = left.Right;
                            expr.Op = left.Op;
                            expr.Lit = left.Lit;
                            isChanged = true;
                           
                            break;
                        }
                    }
                    queue.Enqueue(expr.Left);
                    queue.Enqueue(expr.Right);
                } while (true);
                queue.Clear();
            } while (isChanged);

            return result;
        }

        //metoda zwraca koniunkcje fluentow jako stringi
        public static List<string> GetFluentStrings(BoolExpr tree)
        {
            var list = new List<string>();

            var token = _strings(tree, null, list);

            if(!string.IsNullOrEmpty(token))
                list.Add(token);

            //usuwanie z list a & !a
            return (from s in list let f = s.Split('&') let isOk = f.All(s1 => !f.Contains("not_" + s1)) where isOk select s).ToList();
        }

        static string _strings(BoolExpr expr, BoolExpr parent, List<string> tokens)
        {
            if (expr.IsLeaf())
            {
                if (parent != null && parent.Op == BoolExpr.Bop.Or)
                {
                    tokens.Add(expr.Lit);
                    return "";
                }
                return expr.Lit;
            }
            if (expr.Op == BoolExpr.Bop.Not)
            {
                if (parent != null && parent.Op == BoolExpr.Bop.And)
                    return "not_" + expr.Left.Lit;

                tokens.Add("not_" + expr.Left.Lit);
                return "";

            }
            if (expr.Op == BoolExpr.Bop.And)
            {
                if (parent != null && parent.Op == BoolExpr.Bop.Or)
                    tokens.Add(_strings(expr.Left, expr, tokens) + "&" + _strings(expr.Right, expr, tokens));
                else if (parent != null && parent.Op == BoolExpr.Bop.And)
                    return _strings(expr.Left, expr, tokens) + "&" + _strings(expr.Right, expr, tokens);
                else if(parent == null)
                    tokens.Add(_strings(expr.Left, expr, tokens) + "&" + _strings(expr.Right, expr, tokens));
            }
            if (expr.Left != null)
                _strings(expr.Left, expr, tokens);
            if (expr.Right != null)
                _strings(expr.Right, expr, tokens);
            return "";
        }

        static void TraverseTree(BoolExpr expr, BoolExpr parent, List<string> tokens)
        {
            if (expr.IsLeaf())
            {
                if (parent != null && parent.Op == BoolExpr.Bop.Not)
                {
                    tokens.Add("!" + expr.Lit);
                }
                else
                {
                    tokens.Add(expr.Lit);
                }
                return;
            }
            if (expr.Left != null)
                TraverseTree(expr.Left, expr, tokens);
            if (expr.Right != null)
                TraverseTree(expr.Right, expr, tokens);
        }

        static BoolExpr Make(ref List<Token>.Enumerator polishNotationTokensEnumerator)
        {
            if (polishNotationTokensEnumerator.Current == null)
                throw new ArgumentNullException();

            if (polishNotationTokensEnumerator.Current.Type == Token.TokenType.Literal)
            {
                var lit = BoolExpr.CreateBoolVar(polishNotationTokensEnumerator.Current.Value);
                polishNotationTokensEnumerator.MoveNext();
                return lit;
            }

            switch (polishNotationTokensEnumerator.Current.Value)
            {
                case "NOT":
                    {
                        polishNotationTokensEnumerator.MoveNext();
                        var operand = Make(ref polishNotationTokensEnumerator);
                        return BoolExpr.CreateNot(operand);
                    }
                case "AND":
                    {
                        polishNotationTokensEnumerator.MoveNext();
                        var left = Make(ref polishNotationTokensEnumerator);
                        var right = Make(ref polishNotationTokensEnumerator);
                        return BoolExpr.CreateAnd(left, right);
                    }
                case "OR":
                    {
                        polishNotationTokensEnumerator.MoveNext();
                        var left = Make(ref polishNotationTokensEnumerator);
                        var right = Make(ref polishNotationTokensEnumerator);
                        return BoolExpr.CreateOr(left, right);
                    }
                case "IF":
                    {
                        polishNotationTokensEnumerator.MoveNext();
                        var left = Make(ref polishNotationTokensEnumerator);
                        var right = Make(ref polishNotationTokensEnumerator);
                        return BoolExpr.CreateIf(left, right);
                    }
                case "IFONLYIF":
                    {
                        polishNotationTokensEnumerator.MoveNext();
                        var left = Make(ref polishNotationTokensEnumerator);
                        var right = Make(ref polishNotationTokensEnumerator);
                        return BoolExpr.CreateIfOnlyIf(left, right);
                    }
            }
            return null;
        }

        private static List<Token> TransformToPolishNotation(List<Token> infixTokenList)
        {
            var outputQueue = new Queue<Token>();
            var stack = new Stack<Token>();

            var index = 0;
            while (infixTokenList.Count > index)
            {
                var t = infixTokenList[index];

                switch (t.Type)
                {
                    case Token.TokenType.Literal:
                        outputQueue.Enqueue(t);
                        break;
                    case Token.TokenType.UnaryOp:
                    case Token.TokenType.BinaryOp:
                    case Token.TokenType.OpenParen:
                        stack.Push(t);
                        break;
                    case Token.TokenType.IfOp:
                        Console.WriteLine();
                        while (stack.Count > 0 && stack.Peek().Type != Token.TokenType.OpenParen)
                        {
                            outputQueue.Enqueue(stack.Pop());
                        }
                        stack.Push(t);
                        break;
                    case Token.TokenType.CloseParen:
                        while (stack.Peek().Type != Token.TokenType.OpenParen && stack.Peek().Type != Token.TokenType.IfOp)
                        {
                            outputQueue.Enqueue(stack.Pop());
                        }
                        if (stack.Peek().Type == Token.TokenType.IfOp)
                        {
                            var token = stack.Pop();
                            while (stack.Peek().Type != Token.TokenType.OpenParen)
                            {
                                outputQueue.Enqueue(stack.Pop());
                            }
                            outputQueue.Enqueue(token);
                        }
                        stack.Pop();
                        if (stack.Count > 0 && stack.Peek().Type == Token.TokenType.UnaryOp)
                        {
                            outputQueue.Enqueue(stack.Pop());
                        }
                        break;
                }

                ++index;
            }
            while (stack.Count > 0)
            {
                outputQueue.Enqueue(stack.Pop());
            }

            return outputQueue.Reverse().ToList();
        }

        public static bool Eval(BoolExpr expr, Dictionary<string, bool> fluentValues)
        {
            if (expr.IsLeaf())
            {
                return fluentValues[expr.Lit];
            }

            if (expr.Op == BoolExpr.Bop.Not)
            {
                return !Eval(expr.Left, fluentValues);
            }

            if (expr.Op == BoolExpr.Bop.Or)
            {
                return Eval(expr.Left, fluentValues) || Eval(expr.Right, fluentValues);
            }

            if (expr.Op == BoolExpr.Bop.And)
            {
                return Eval(expr.Left, fluentValues) && Eval(expr.Right, fluentValues);
            }

            if (expr.Op == BoolExpr.Bop.If)
            {
                return !Eval(expr.Left, fluentValues) || Eval(expr.Right, fluentValues);
            }

            if (expr.Op == BoolExpr.Bop.IfOnlyIf)
            {
                return (!Eval(expr.Left, fluentValues) || Eval(expr.Right, fluentValues)) && (Eval(expr.Left, fluentValues) || !Eval(expr.Right, fluentValues));
            }

            throw new ArgumentException();
        }
    }

    public class Token
    {
        private static readonly Dictionary<string, KeyValuePair<TokenType, string>> Dict = new Dictionary<string, KeyValuePair<TokenType, string>>
        {
        {
            "(", new KeyValuePair<TokenType, string>(TokenType.OpenParen, "(")
        },
        {
            ")", new KeyValuePair<TokenType, string>(TokenType.CloseParen, ")")
        },
        {
            "!", new KeyValuePair<TokenType, string>(TokenType.UnaryOp, "NOT")
        },
        {
            "&", new KeyValuePair<TokenType, string>(TokenType.BinaryOp, "AND")
        },
        {
            "|", new KeyValuePair<TokenType, string>(TokenType.BinaryOp, "OR")
        },
        {
            "->", new KeyValuePair<TokenType, string>(TokenType.IfOp, "IF")
        },
        {
            "<->", new KeyValuePair<TokenType, string>(TokenType.IfOp, "IFONLYIF")
        }
    };

        public enum TokenType
        {
            OpenParen,
            CloseParen,
            IfOp,
            UnaryOp,
            BinaryOp,
            Literal,
            ExprEnd
        }

        public readonly TokenType Type;
        public readonly string Value;

        public Token(StringReader s)
        {
            var c = s.Read();
            if (c == -1)
            {
                Type = TokenType.ExprEnd;
                Value = "";
                return;
            }

            var ch = ((char)c).ToString(CultureInfo.InvariantCulture);

            if (Dict.ContainsKey(ch))
            {
                Type = Dict[ch].Key;
                Value = Dict[ch].Value;
            }
            else
            {
                var str = "";
                str += ch;
                while (s.Peek() != -1 && !Dict.ContainsKey(((char)s.Peek()).ToString(CultureInfo.InvariantCulture)) && ((char)s.Peek() != '-' || ((char)s.Peek() == '-' && str == "<")) && (char)s.Peek() != '<')
                {
                    str += (char)s.Read();

                    if (Dict.ContainsKey(str))
                    {
                        Type = Dict[str].Key;
                        Value = Dict[str].Value;
                        return;
                    }
                }
                Type = TokenType.Literal;
                Value = str;
            }
        }
    }

    public class BoolExpr
    {
        public enum Bop { Leaf, And, Or, Not, If, IfOnlyIf };

        //
        //  inner state
        //

        private Bop _op;
        private BoolExpr _left;
        private BoolExpr _right;
        private String _lit;

        //
        //  private constructor
        //

        private BoolExpr(Bop op, BoolExpr left, BoolExpr right)
        {
            _op = op;
            _left = right;
            _right = left;
            _lit = null;
        }

        private BoolExpr(String literal)
        {
            _op = Bop.Leaf;
            _left = null;
            _right = null;
            _lit = literal;
        }

        //
        //  accessor
        //

        public Bop Op
        {
            get { return _op; }
            set { _op = value; }
        }

        public BoolExpr Left
        {
            get { return _left; }
            set { _left = value; }
        }

        public BoolExpr Right
        {
            get { return _right; }
            set { _right = value; }
        }

        public String Lit
        {
            get { return _lit; }
            set { _lit = value; }
        }

        //
        //  public factory
        //

        public static BoolExpr CreateAnd(BoolExpr left, BoolExpr right)
        {
            return new BoolExpr(Bop.And, left, right);
        }

        public static BoolExpr CreateIf(BoolExpr left, BoolExpr right)
        {
            return new BoolExpr(Bop.If, left, right);
        }

        public static BoolExpr CreateIfOnlyIf(BoolExpr left, BoolExpr right)
        {
            return new BoolExpr(Bop.IfOnlyIf, left, right);
        }

        public static BoolExpr CreateNot(BoolExpr child)
        {
            return new BoolExpr(Bop.Not, null, child);
        }

        public static BoolExpr CreateOr(BoolExpr left, BoolExpr right)
        {
            return new BoolExpr(Bop.Or, left, right);
        }

        public static BoolExpr CreateBoolVar(String str)
        {
            return new BoolExpr(str);
        }

        public BoolExpr(BoolExpr other)
        {
            // No share any object on purpose
            _op = other._op;
            _left = other._left == null ? null : new BoolExpr(other._left);
            _right = other._right == null ? null : new BoolExpr(other._right);
            _lit = new StringBuilder(other._lit).ToString();
        }

        //
        //  state checker
        //

        public Boolean IsLeaf()
        {
            return (_op == Bop.Leaf);
        }

        

        /*
                Boolean IsAtomic()
                {
                    return (IsLeaf() || (_op == Bop.Not && _left.IsLeaf()));
                }
        */
    }
}