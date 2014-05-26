using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Rw.AdeSystem.Core
{
    public static class LogicFormulaParser
    {
        public static BoolExpr Parse(string expr, out List<Token> literals)
        {
            literals = new List<Token>();
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

            return root;
            //Eval the expression tree
            //Console.WriteLine(@"Eval: {0}", Eval(root));
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
                    case Token.TokenType.BinaryOp:
                    case Token.TokenType.UnaryOp:
                    case Token.TokenType.OpenParen:
                        stack.Push(t);
                        break;
                    case Token.TokenType.CloseParen:
                        while (stack.Peek().Type != Token.TokenType.OpenParen)
                        {
                            outputQueue.Enqueue(stack.Pop());
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

        

        static bool Eval(BoolExpr expr)
        {
            if (expr.IsLeaf())
            {
                return false;//booleanValues[expr.Lit];
            }

            if (expr.Op == BoolExpr.Bop.Not)
            {
                return !Eval(expr.Left);
            }

            if (expr.Op == BoolExpr.Bop.Or)
            {
                return Eval(expr.Left) || Eval(expr.Right);
            }

            if (expr.Op == BoolExpr.Bop.And)
            {
                return Eval(expr.Left) && Eval(expr.Right);
            }

            throw new ArgumentException();
        }
    }

    //abstract class Ex
    //{
    //    public abstract bool Evaluate();
    //}

    //class LeafEx : Ex
    //{
    //    override public bool Evaluate()
    //    {
    //        return true; //Boolean.Parse(this.Lit);
    //    }
    //}

    //class NotEx : Ex
    //{
    //    public Ex Left { get; set; }
    //    override public bool Evaluate()
    //    {
    //        return !Left.Evaluate();
    //    }
    //}

    //class OrEx : Ex
    //{
    //    public Ex Left { get; set; }
    //    public Ex Right { get; set; }

    //    override public bool Evaluate()
    //    {
    //        return Left.Evaluate() || Right.Evaluate();
    //    }
    //}

    //class AndEx : Ex
    //{
    //    public Ex Left { get; set; }
    //    public Ex Right { get; set; }

    //    override public bool Evaluate()
    //    {
    //        return Left.Evaluate() && Right.Evaluate();
    //    }
    //}

    public class Token
    {
        static readonly Dictionary<char, KeyValuePair<TokenType, string>> Dict = new Dictionary<char, KeyValuePair<TokenType, string>>
        {
        {
            '(', new KeyValuePair<TokenType, string>(TokenType.OpenParen, "(")
        },
        {
            ')', new KeyValuePair<TokenType, string>(TokenType.CloseParen, ")")
        },
        {
            '!', new KeyValuePair<TokenType, string>(TokenType.UnaryOp, "NOT")
        },
        {
            '&', new KeyValuePair<TokenType, string>(TokenType.BinaryOp, "AND")
        },
        {
            '|', new KeyValuePair<TokenType, string>(TokenType.BinaryOp, "OR")
        }
    };

        public enum TokenType
        {
            OpenParen,
            CloseParen,
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

            var ch = (char)c;

            if (Dict.ContainsKey(ch))
            {
                Type = Dict[ch].Key;
                Value = Dict[ch].Value;
            }
            else
            {
                //TODO: tu bedzie trzeba zmienic cus zeby kilka znakow czytalo
                var str = "";
                str += ch;
                while (s.Peek() != -1 && !Dict.ContainsKey((char)s.Peek()))
                {
                    str += (char)s.Read();
                }
                Type = TokenType.Literal;
                Value = str;
            }
        }
    }

    public class BoolExpr
    {
        public enum Bop { Leaf, And, Or, Not };

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
            _left = left;
            _right = right;
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

        public static BoolExpr CreateNot(BoolExpr child)
        {
            return new BoolExpr(Bop.Not, child, null);
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