:-  dynamic(releases/4),dynamic(preserve/3).

neg(X,Y) :- sneg(Y,X).
neg(X,Y) :- sneg(X,Y).

neg(X,X) :- !,fail.

inertial(X) :- sinertial(X).
inertial(X) :- neg(X,Y),
               sinertial(Y).

calculate([], _, _,[]).
calculate(STATES,[],[],STATES).
calculate([STATE_FROM|STATES_FROM], [ACTION|ACTIONS], [EXECUTOR|EXECUTORS], STATES_TO) :-
    calculate(STATES_FROM, [ACTION|ACTIONS], [EXECUTOR|EXECUTORS], STATES_TO2),
    res0_trunc(ACTION, EXECUTOR, STATE_FROM, STATES),
    calculate(STATES, ACTIONS, EXECUTORS, STATES2),
    subtract(STATES2, STATES_TO2,STATES_TO_ADD),
    append(STATES_TO2,STATES_TO_ADD,STATES_TO),
    !.

always_executable([], _, _,[]).
always_executable(STATES,[],[],STATES).
always_executable([STATE_FROM|STATES_FROM], [ACTION|ACTIONS], [EXECUTOR|EXECUTORS], STATES_TO) :-
    always_executable(STATES_FROM, [ACTION|ACTIONS], [EXECUTOR|EXECUTORS], STATES_TO2),
    res0_trunc(ACTION, EXECUTOR, STATE_FROM, STATES),
    length(STATES, STATES_LENGTH),
    STATES_LENGTH > 0, % jeżeli chociaż jedno, gdziekolwiek będzie miało pusty wysypie sie całość
    always_executable(STATES, ACTIONS, EXECUTORS, STATES2),
    subtract(STATES2, STATES_TO2,STATES_TO_ADD),
    append(STATES_TO2,STATES_TO_ADD,STATES_TO),
    !.

possibly_executable([], _, _,[]).
possibly_executable(STATES,[],[],STATES).
possibly_executable([STATE_FROM|STATES_FROM], [ACTION|ACTIONS], [EXECUTOR|EXECUTORS], STATES_TO) :-
    possibly_executable(STATES_FROM, [ACTION|ACTIONS], [EXECUTOR|EXECUTORS], STATES_TO2),
    res0_trunc(ACTION, EXECUTOR, STATE_FROM, STATES),
    possibly_executable(STATES, ACTIONS, EXECUTORS, STATES2),
    subtract(STATES2, STATES_TO2,STATES_TO_ADD),
    append(STATES_TO2,STATES_TO_ADD,STATES_TO),
    length(STATES_TO, COUNT),
    COUNT > 0, % jeżeli nie dostaliśmy nic z startów początkowych - nie ma żadnego wykonywalnego dla tego poziomu.
    !.

pair_lists([],[],[]).
pair_lists([HEAD1|L1], [HEAD2|L2], [ELEM|X]) :-
    ELEM = [HEAD1,HEAD2],
    pair_lists(L1,L2,X).

all_calculated_states(X) :-
    findall(Y,fluent(Y), R),
    convert_negatives(R,R2),
    pair_lists(R,R2,R3),
    prod(R3,X2),
    append_fluents([],X2,X), % przy okazji sortujemy fluenty ;)
    !.


accRev([H|T],A,R):-  accRev(T,[H|A],R).
accRev([],A,A). 

reverse(LIST1, LIST2) :- accRev(LIST1,[],LIST2).

reorder_fluents(LIST, RES) :-
    length(LIST, LENGTH),
    LENGTH2 is LENGTH - 1,
    reorder_fluents_cont(LIST, RES2, LENGTH2),
    reverse(RES2,RES),
    !.

reorder_fluents_cont(_, [], -1).

reorder_fluents_cont(LIST, [HEAD2|LIST2], POS) :-
    POS > -1,
    order(POS, FLUENT),
    (
        member(FLUENT, LIST),
        HEAD2 = FLUENT
    ;
        not(member(FLUENT,LIST)),
        neg(FLUENT, NEGATIVE),
        HEAD2 = NEGATIVE
    ),
    POS2 is POS - 1,
    reorder_fluents_cont(LIST, LIST2, POS2),
    !.

append_fluents(_, [], []).

append_fluents(FLUENTS, [HEAD|L], [ELEM|L2]) :-
    append(FLUENTS, HEAD, ELEM2),
    reorder_fluents(ELEM2, ELEM),
    append_fluents(FLUENTS, L, L2).

normalize([],[]).

normalize([HEAD|FLUENTS], [HEAD|NORMALIZED]) :-
    fluent(HEAD),
    normalize(FLUENTS, NORMALIZED).

normalize([HEAD|FLUENTS], [NEG|NORMALIZED]) :-
    not(fluent(HEAD)),
    neg(HEAD, NEG),
    normalize(FLUENTS, NORMALIZED).


all_calculated_states(FLUENTS, X) :-
    normalize(FLUENTS, POSITIVE_FLUENTS),
    findall(A, fluent(A), ALL_FLUENTS),
    subtract(ALL_FLUENTS, POSITIVE_FLUENTS, REST),
    convert_negatives(REST, NEGATIVES),
    pair_lists(REST, NEGATIVES, LISTS),
    prod(LISTS, X2),
    append_fluents(FLUENTS, X2, X).

all_calculated_states(STATES,FLUENTS, X) :-
    normalize(FLUENTS, POSITIVE_FLUENTS),
    findall(A, fluent(A), ALL_FLUENTS),
    subtract(ALL_FLUENTS, POSITIVE_FLUENTS, REST),
    convert_negatives(REST, NEGATIVES),
    pair_lists(REST, NEGATIVES, LISTS),
    prod(LISTS, X2),
    append_fluents(FLUENTS, X2, X3),
    subtract(X3, STATES, STATES_NOT_IN),
    subtract(X3, STATES_NOT_IN,X).


noninertial(X) :- not(inertial(X)).

released_fluents(ACTION, EXECUTOR, STATE_FROM, OUTPUT) :-
    findall([X,Y], releases(ACTION, EXECUTOR,X,Y),R),
    released_fluents_continue(STATE_FROM, R, OUTPUT).

released_fluents_continue(_,[],[]).

released_fluents_continue(STATE, [HEAD|FLUENTS], OUTPUT) :-
    released_fluents_continue(STATE, FLUENTS, OUTPUT2),
    nth0(0, HEAD, FLUENT),
    nth0(1, HEAD, FORMULA),
    state_valid_with_formula(STATE, FORMULA),
    append(FLUENT, OUTPUT2,OUTPUT),
    !.

released_fluents_continue(STATE, [HEAD|FLUENTS], OUTPUT) :-
    released_fluents_continue(STATE, FLUENTS, OUTPUT2),
    nth0(1, HEAD, FORMULA),
    not(state_valid_with_formula(STATE, FORMULA)),
    OUTPUT = OUTPUT2,
    !.

release_fluent([], _,[]).

release_fluent([HEAD|STATES], FLUENT, OUTPUT) :- 
    release_fluent(STATES, FLUENT, OUTPUT2),
    neg(FLUENT, NEG_FLUENT),
    state(HEAD,STATE_LIST),
    force_cause_change([FLUENT], STATE_LIST, STATE1_LIST),
    force_cause_change([NEG_FLUENT], STATE_LIST, STATE2_LIST),
    nth0(0,STATE1_LIST,S1),
    nth0(0,STATE2_LIST,S2),
    join_possible_states(OUTPUT2, [S1,S2], OUTPUT),
    !. 

join_possible_states(LIST, [], LIST) :- !.

join_possible_states(LIST, [HEAD|REST], [STATE|OUTPUT]) :- 
    state(STATE, HEAD),not(member(STATE,LIST)),
    join_possible_states(LIST, REST, OUTPUT),
    !.

join_possible_states(LIST, [HEAD|REST], OUTPUT) :- 
    (not(state(_, HEAD));member(_,LIST)),
    join_possible_states(LIST, REST, OUTPUT),
    !.


release_fluents(STATES, [], STATES).

release_fluents(STATES, [HEAD|FLUENTS], OUTPUT) :-
    release_fluents(STATES, FLUENTS, OUTPUT1),
    release_fluent(OUTPUT1, HEAD, OUTPUT),
    !.


formula_valid(FORMULA, FLUENTS) :- 
    formula(FORMULA, STMT_LIST),
    formula_valid_continue(STMT_LIST, FLUENTS).

formula_valid_continue([],_) :- !,fail.

formula_valid_continue([HEAD|_], FLUENTS) :-
    stmt(HEAD, HEAD_LIST),
    subset(HEAD_LIST,FLUENTS).

formula_valid_continue([HEAD|STMT], FLUENTS) :-
    stmt(HEAD, HEAD_LIST),
    not(subset(HEAD_LIST,FLUENTS)),
    formula_valid_continue(STMT, FLUENTS).

state_valid(X) :-
    findall([Y], always(Y), R),
    state_valid_continue(R, X),
    !.

state_valid_continue([HEAD|FORMULAS], STATE) :-
    nth0(0, HEAD, FORMULA),
    state_valid_with_formula(STATE, FORMULA),
    state_valid_continue(FORMULAS,STATE),
    !.

state_valid_continue([], _).

state_valid_with_formula(STATE, FORMULA) :-
    state(STATE, FLUENTS),
    formula_valid(FORMULA, FLUENTS).

fluents_valid(X) :-
    findall([Y], always(Y), R),
    fluents_valid_continue(R, X).

fluents_valid_continue([HEAD|FORMULAS], FLUENTS) :-
    nth0(0, HEAD, FORMULA),
    formula_valid(FORMULA, FLUENTS),
    fluents_valid_continue(FORMULAS, FLUENTS).

fluents_valid_continue([], _).

initial_states(STATES) :- 
    initially(X),
    all_possible_states(X,STATES).

list_of_states(R) :- findall(X, state(X,_),R).

possible_state(LIST_OF_FLUENTS, STATE) :-
    state(STATE, LIST_OF_FLUENTS_OF_STATE),
    subset(LIST_OF_FLUENTS, LIST_OF_FLUENTS_OF_STATE).

possible_states(_, [], []).
possible_states(LIST_OF_FLUENTS, [HEAD|STATES], [HEAD|POSSIBLE_STATES]) :-
    possible_state(LIST_OF_FLUENTS, HEAD),
    possible_states(LIST_OF_FLUENTS, STATES, POSSIBLE_STATES).

possible_states(LIST_OF_FLUENTS, [HEAD|STATES], POSSIBLE_STATES) :-
    not(possible_state(LIST_OF_FLUENTS, HEAD)),
    possible_states(LIST_OF_FLUENTS, STATES, POSSIBLE_STATES).

all_possible_states(LIST_OF_FLUENTS,X) :- 
    list_of_states(LIST_OF_ALL_STATES),
    possible_states(LIST_OF_FLUENTS, LIST_OF_ALL_STATES, X).


force_cause_change(LIST,STATE1_LIST,STATE2_LIST) :-
    all_calculated_states(LIST, STATE_LISTS),
    filter_only_correct_states(STATE_LISTS, CORRECT_LISTS),
    minimal_length_new_of_res0(CORRECT_LISTS, STATE1_LIST, MINIMAL),
    copy_res0_state_if_minimal_new(CORRECT_LISTS, STATE1_LIST, MINIMAL, STATE2_LIST).


possible_causes_fto_states(ACTION, EXECUTOR, STATE1, STATE2) :-
    findall([X,Y], causes(ACTION, EXECUTOR, X, Y), R1),
    findall([X,Y], causes(ACTION, epsilon, X, Y), R2),
    append(R1,R2,R),
    state(STATE1, STATE1_LIST),
    state(STATE2, STATE2_LIST),
    possible_causes_fto_states2(R, STATE1_LIST, STATE2_LIST).

possible_typically_causes_fto_states(ACTION, EXECUTOR, STATE1, STATE2) :-
    findall([X,Y], typically_causes(ACTION, EXECUTOR, X, Y), R1),
    findall([X,Y], typically_causes(ACTION, epsilon, X, Y), R2),
    append(R1,R2,R),
    length(R,R_LENGTH),
    R_LENGTH > 0,
    state(STATE1, STATE1_LIST),
    state(STATE2, STATE2_LIST),
    possible_causes_fto_states2(R, STATE1_LIST, STATE2_LIST).

possible_typically_causes_fto_states(ACTION, EXECUTOR, STATE1, STATE2) :-
    findall([X,Y], typically_causes(ACTION, EXECUTOR, X, Y), R1),
    findall([X,Y], typically_causes(ACTION, epsilon, X, Y), R2),
    append(R1,R2,R),
    length(R,R_LENGTH),
    R_LENGTH == 0,
    findall([X,Y], causes(ACTION, EXECUTOR, X, Y), R3),
    findall([X,Y], causes(ACTION, epsilon, X, Y), R4),
    append(R3,R4,R5),
    state(STATE1, STATE1_LIST),
    state(STATE2, STATE2_LIST),
    possible_causes_fto_states2(R5, STATE1_LIST, STATE2_LIST).


possible_causes_fto_states2([], _, _).
possible_causes_fto_states2([HEAD|TAIL],LIST1, LIST2) :-
    nth0(0, HEAD, HEAD1),
    nth0(1, HEAD, HEAD2),
    (not(subset(HEAD2, LIST2)); subset(HEAD1, LIST1)),
    possible_causes_fto_states2(TAIL, LIST1, LIST2).

filter_active(_, [], []).

filter_active(STATE, [HEAD|CAUSES_ALL], [HEAD|CAUSES_ACTIVE]) :-
    nth0(1, HEAD, ACTION_REQ),
    subset(ACTION_REQ, STATE),
    filter_active(STATE, CAUSES_ALL, CAUSES_ACTIVE),
    !.

filter_active(STATE, [HEAD|CAUSES_ALL], CAUSES_ACTIVE) :-
    nth0(1, HEAD, ACTION_REQ),
    not(subset(ACTION_REQ, STATE)),
    filter_active(STATE, CAUSES_ALL, CAUSES_ACTIVE),
    !.

merge_results([],[]).

merge_results([HEAD|CAUSES], RESULTS) :-
    nth0(0, HEAD, RESULTS1),
    merge_results(CAUSES, RESULTS2),
    append(RESULTS1, RESULTS2,RESULTS).

convert_list_to_state([],[]).
convert_list_to_state([HEAD1|LISTS], [HEAD2|STATES]) :-
    state(HEAD2,HEAD1),
    convert_list_to_state(LISTS,STATES).

res0(ACTION, EXECUTOR, STATE, STATES) :-
    findall([X,Y], causes(ACTION, EXECUTOR, X,Y),R),
    state(STATE, STATE_FLUENTS),
    filter_active(STATE_FLUENTS, R, R_ACTIVE),
    merge_results(R_ACTIVE, RESULTS),
    all_calculated_states(RESULTS, STATES_LIST),
    filter_only_correct_states(STATES_LIST,STATES_LIST2),
    convert_list_to_state(STATES_LIST2, STATES2),
    sort(STATES2,STATES),
    !.

filter_only_correct_states([],[]).

filter_only_correct_states([HEAD|STATE_LISTS], [HEAD|CORRECT]) :-
    fluents_valid(HEAD),
    filter_only_correct_states(STATE_LISTS,CORRECT),
    !.

filter_only_correct_states([HEAD|STATE_LISTS], CORRECT) :-
    not(fluents_valid(HEAD)),
    filter_only_correct_states(STATE_LISTS,CORRECT),
    !.


res0_continue(_,_,_,[],[]).

res0_continue(ACTION, EXECUTOR, STATE, [HEAD|ALL], [HEAD|STATES]) :-
    possible_causes_fto_states(ACTION, EXECUTOR, HEAD, STATE),
    res0_continue(ACTION, EXECUTOR, STATE, ALL, STATES),
    !.

res0_continue(ACTION, EXECUTOR, STATE, [HEAD|ALL], STATES) :-
    not(possible_causes_fto_states(ACTION, EXECUTOR, HEAD, STATE)),
    res0_continue(ACTION, EXECUTOR, STATE, ALL, STATES),
    !.

convert_states_to_lists([],[]).

convert_states_to_lists([HEAD|STATE], [ELEM|LISTS]) :-
    state(HEAD,ELEM),
    convert_states_to_lists(STATE,LISTS).

res0_plus(ACTION, EXECUTOR, STATE, STATES) :-
    findall([X,Y], typically_causes(ACTION, EXECUTOR, X,Y),R1),
    length(R1, R1_LENGTH),
    (
        R1_LENGTH > 0 -> R = R1
    ;
        R1_LENGTH == 0,
        findall([X,Y],causes(ACTION, EXECUTOR, X,Y),R2),
        R = R2
    ),
    state(STATE, STATE_FLUENTS),
    filter_active(STATE_FLUENTS, R, R_ACTIVE),
    merge_results(R_ACTIVE, RESULTS),
    res0(ACTION, EXECUTOR, STATE,STATES_0),
    convert_states_to_lists(STATES_0, STATES_0_LISTS),
    all_calculated_states(STATES_0_LISTS,RESULTS, STATES_LIST),
    filter_only_correct_states(STATES_LIST,STATES_LIST2),
    convert_list_to_state(STATES_LIST2, STATES2),
    sort(STATES2,STATES),
    !.


res0_plus_continue(_,_,_,[],[]).

res0_plus_continue(ACTION, EXECUTOR, STATE, [HEAD|ALL], [HEAD|STATES]) :-
    possible_typically_causes_fto_states(ACTION, EXECUTOR, HEAD, STATE),
    res0_plus_continue(ACTION, EXECUTOR, STATE, ALL, STATES),
    !.

res0_plus_continue(ACTION, EXECUTOR, STATE, [HEAD|ALL], STATES) :-
    not(possible_typically_causes_fto_states(ACTION, EXECUTOR, HEAD, STATE)),
    res0_plus_continue(ACTION, EXECUTOR, STATE, ALL, STATES),
    !.

new(LIST1, LIST2,RELEASED, OUTPUT) :-
    values_of_state(LIST1, RELEASED, RELEASEDX),
    subtract(LIST1, LIST2, CHANGE),
    findall(X,fluent(X),FLUENTS),
    findall(Y,sinertial(Y),INERTIAL),
    subtract(FLUENTS, INERTIAL, NONINERTIAL2),
    convert_negatives(NONINERTIAL2, NEGNONINERTIAL2),
    append(NONINERTIAL2, NEGNONINERTIAL2, NONINERTIAL),
    subtract(CHANGE, NONINERTIAL, CHANGE2),
    subtract(RELEASEDX, CHANGE2, RELEASED2),
    append(CHANGE2, RELEASED2, OUTPUT).

values_of_state(_,[],[]).

values_of_state(STATE_LIST, [HEAD1|FLUENTS], [HEAD2|OUTPUT]) :-
    member(HEAD1, STATE_LIST),
    HEAD2 = HEAD1,
    values_of_state(STATE_LIST, FLUENTS, OUTPUT).

values_of_state(STATE_LIST, [HEAD1|FLUENTS], [HEAD2|OUTPUT]) :-
    not(member(HEAD1, STATE_LIST)),
    neg(HEAD1,HEAD2),
    values_of_state(STATE_LIST, FLUENTS, OUTPUT).

res0_min(ACTION, EXECUTOR, STATE, STATES) :-
   res0(ACTION, EXECUTOR,STATE, STATES_0),
   convert_states_to_lists(STATES_0,STATES_0LIST),
   state(STATE,STATE_LIST),
   released_fluents(ACTION, EXECUTOR, STATE, FLUENTS),
   minimals(STATES_0LIST, STATE_LIST, FLUENTS,MINIMAL),
   convert_list_to_state(MINIMAL, STATES2),
   sort(STATES2,STATES),
   !.

resN(ACTION, EXECUTOR, STATE, STATES) :-
   res0_plus(ACTION, EXECUTOR,STATE, STATES_0),
   convert_states_to_lists(STATES_0,STATES_0LIST),
   state(STATE,STATE_LIST),
   released_fluents(ACTION, EXECUTOR, STATE, FLUENTS),
   minimals(STATES_0LIST, STATE_LIST, FLUENTS,MINIMAL),
   convert_list_to_state(MINIMAL, STATES2),
   sort(STATES2,STATES),
   !.


minimals(STATES, STATE, FLUENTS, OUTPUT) :-
    calc_new(STATES, STATE, FLUENTS, LIST),
    minimals_sets(LIST, OUTPUT).

calc_new([],_,_,[]).

calc_new([HEAD|STATES], STATE, FLUENTS, [NEW_SET|OUTPUT]) :-
        new(HEAD, STATE, FLUENTS, NEW),
        NEW_SET = [HEAD, NEW],
        calc_new(STATES, STATE, FLUENTS, OUTPUT).

minimals_sets(LIST, MINIMALS) :-
    findall(Y1,
    (
        member(Y,LIST),
        nth0(0,Y,Y1),
        nth0(1,Y,Y2),
        forall(
        (
            member(X,LIST),
            nth0(1,X,X2),
            X2\=Y2
        ),
        not(subset(X2,Y2)
        ))),
    MINIMALS).

states_valid([],[]).

states_valid([HEAD|X],[HEAD|Y]) :- 
    state_valid(HEAD),
    !,
    states_valid(X,Y).

states_valid([HEAD|X],Y) :- 
    not(state_valid(HEAD)),
    !,
    states_valid(X,Y).

fluent_values(STATE, FLUENTS, VALUES) :-
    state(STATE, STATE_FLUENTS),
    subtract(FLUENTS, STATE_FLUENTS,  NEGATIVE),
    subtract(FLUENTS, NEGATIVE, POSITIVE),
    convert_negatives(NEGATIVE, NPOSITIVES),
    append(POSITIVE, NPOSITIVES, VALUES).

convert_negatives([], []).

convert_negatives([HEAD|FLUENTS], [CONVERTED|NFLUENTS]) :-
    neg(HEAD, CONVERTED),
    convert_negatives(FLUENTS, NFLUENTS).


preserve_fluents(ACTION, EXECUTOR, STATE_FROM, STATES_TO, OUTPUT) :-
    preserve(ACTION, EXECUTOR, PRESERVED),
    fluent_values(STATE_FROM, PRESERVED, VALUES),
    all_possible_states(VALUES, POSSIBLE_STATES),
    subtract(STATES_TO,POSSIBLE_STATES, STATES_TO_NOT_ALLOWED),
    subtract(STATES_TO, STATES_TO_NOT_ALLOWED, OUTPUT),
    !.

preserve_fluents(ACTION, EXECUTOR, _, STATES_TO, STATES_TO) :-
    not(preserve(ACTION, EXECUTOR, _)).

% przypadek nie ma preserve dla danej akcji


resN_trunc(ACTION, EXECUTOR, STATE, STATES) :-
    resN(ACTION, EXECUTOR, STATE, STATES2),
    states_valid(STATES2, STATES3),
    preserve_fluents(ACTION, EXECUTOR, STATE, STATES3,STATES).

resAb(ACTION,EXECUTOR, STATE, STATES) :-
    res0_min(ACTION, EXECUTOR, STATE, STATES0),
    resN(ACTION, EXECUTOR, STATE, STATESN),
    subtract(STATES0, STATESN, STATES3),
    sort(STATES3,STATES),
    !.

resAb_trunc(ACTION, EXECUTOR, STATE, STATES) :-
    resAb(ACTION, EXECUTOR, STATE, STATES2),
    states_valid(STATES2, STATES3),
    preserve_fluents(ACTION, EXECUTOR, STATE,STATES3,STATES).

res0_trunc(ACTION, EXECUTOR, STATE, STATES) :-
    resN_trunc(ACTION, EXECUTOR, STATE, STATES1),
    resAb_trunc(ACTION, EXECUTOR, STATE, STATES2),
    append(STATES1, STATES2, STATES).


action_causes([], _,_, []).
action_causes([HEAD|STATES_FROM], ACTION, EXECUTOR, STATES_TO) :- 
    action_causes(STATES_FROM, ACTION, EXECUTOR, STATES_TO2),
    res0_min(ACTION, EXECUTOR, HEAD,STATES),
    subtract(STATES,STATES_TO2,STATES2),
    append(STATES2,STATES_TO2,STATES_TO).

actions_causes(STATES,[],[],STATES).

actions_causes(STATES_FROM, [ACTION|ACTIONS], [EXECUTOR|EXECUTORS], STATES_TO) :- 
    action_causes(STATES_FROM, ACTION, EXECUTOR, STATES_TO1),
    actions_causes(STATES_TO1, ACTIONS, EXECUTORS, STATES_TO).

pexecutable(STATE, ACTION, EXECUTOR) :-
    res0_trunc(ACTION, EXECUTOR, STATE, X),
    length(X,Y),
    Y > 0.

possible_executable(ACTION, EXECUTOR, FLUENTS) :-
    all_possible_states(FLUENTS, STATES),
    possible_executable_continue(ACTION, EXECUTOR, STATES).

possible_executable_continue(ACTION, EXECUTOR, [HEAD|_]) :-   
    pexecutable(HEAD, ACTION, EXECUTOR).

possible_executable_continue(ACTION, EXECUTOR, [HEAD|STATES]) :-   
    not(pexecutable(HEAD, ACTION, EXECUTOR)),
    possible_executable_continue(ACTION, EXECUTOR, STATES).

always_executable(ACTION, EXECUTOR, FLUENTS) :- 
    all_possible_states(FLUENTS, STATES),
    always_executable_continue(ACTION, EXECUTOR, STATES).

always_executable_continue(ACTION, EXECUTOR, [HEAD|STATES]) :-   
    pexecutable(HEAD, ACTION, EXECUTOR),
    always_executable_continue(ACTION, EXECUTOR, STATES).

always_executable_continue(_, _, []).


always_executable_continue([],_,_).

always_accessible(GOAL, FLUENTS) :-
    all_possible_states(FLUENTS, STATES_FROM),
    always_accessible_continue(STATES_FROM,[], GOAL),
    !.


always_accessible_continue([], _, _).

always_accessible_continue([HEAD|NOT_VISITED], VISITED, GOAL) :-
    state(HEAD, FLUENTS),
    subset(GOAL, FLUENTS),
    always_accessible_continue(NOT_VISITED, VISITED, GOAL),
    !.

always_accessible_continue([HEAD|NOT_VISITED], VISITED, GOAL) :-
    state(HEAD, FLUENTS),
    not(subset(GOAL, FLUENTS)),
    findall([X,Y,Z,Z2], causes(X,Y,Z,Z2),R1),
    findall([X,Y,Z,Z2], typically_causes(X,Y,Z,Z2),R2),
    append(R1,R2,R),
    member(MOVE, R),
    nth0(0, MOVE, ACTION),
    nth0(1, MOVE, EXECUTOR),
    res0_trunc(ACTION, EXECUTOR, HEAD, STATES),
    subtract(STATES, [HEAD|VISITED], STATES_TO_VISIT),
    length(STATES, S_LENGTH),
    S_LENGTH > 0,
    length(STATES_TO_VISIT, STATES_TO_VISIT_LENGTH),
    STATES_TO_VISIT_LENGTH > 0,
    always_accessible_continue(STATES_TO_VISIT, [HEAD|VISITED], GOAL),
    always_accessible_continue(NOT_VISITED, VISITED, GOAL),
    !.

typically_accessible(GOAL, FLUENTS) :-
    all_possible_states(FLUENTS, STATES_FROM),
    typically__accessible_continue(STATES_FROM,[], GOAL),
    !.


typically_accessible_continue([], _, _).

typically_accessible_continue([HEAD|NOT_VISITED], VISITED, GOAL) :-
    state(HEAD, FLUENTS),
    subset(GOAL, FLUENTS),
    typically_accessible_continue(NOT_VISITED, VISITED, GOAL),
    !.

typically_accessible_continue([HEAD|NOT_VISITED], VISITED, GOAL) :-
    state(HEAD, FLUENTS),
    not(subset(GOAL, FLUENTS)),
    findall([X,Y,Z,Z2], typically_causes(X,Y,Z,Z2),R),
    member(MOVE, R),
    nth0(0, MOVE, ACTION),
    nth0(1, MOVE, EXECUTOR),
    resN_trunc(ACTION, EXECUTOR, HEAD, STATES),
    subtract(STATES, [HEAD|VISITED], STATES_TO_VISIT),
    length(STATES, S_LENGTH),
    S_LENGTH > 0,
    length(STATES_TO_VISIT, STATES_TO_VISIT_LENGTH),
    STATES_TO_VISIT_LENGTH > 0,
    typically_accessible_continue(STATES_TO_VISIT, [HEAD|VISITED], GOAL),
    typically_accessible_continue(NOT_VISITED, VISITED, GOAL),
    !.


all_continue_ways_check([], _,_,_).
all_continue_ways_check([STATES2|POSSIBLE_CONT], [HEAD|NOT_VISITED], VISITED, GOAL) :-
     list_to_set(STATES2,STATES),
     subtract(STATES, [HEAD|VISITED], TO_BE_VISITED),
     subtract(TO_BE_VISITED, NOT_VISITED, TO_BE_VISITED2),
     append(NOT_VISITED, TO_BE_VISITED2, NOT_VISITED2),
     always_accessible_continue(NOT_VISITED2, [HEAD | VISITED], GOAL),
     all_continue_ways_check(POSSIBLE_CONT, [HEAD|NOT_VISITED], VISITED, GOAL).


get_res_list_for_causes(_,[],[]).

get_res_list_for_causes(STATE, [HEAD | CAUSES], LIST_OF_RES) :-
    get_res_list_for_causes(STATE, CAUSES, LIST_OF_RES2),
    nth0(0, HEAD, ACTION),
    nth0(1, HEAD, EXECUTOR),
    res0_min(ACTION, EXECUTOR, STATE, X),
    length(X, X_LENGTH),
    (
        (
            X_LENGTH > 0,
            append(LIST_OF_RES2, [X], LIST_OF_RES)
        )
    ;
        (
            X_LENGTH == 0,
            LIST_OF_RES = LIST_OF_RES2
        )
    )
    ,!.


possibly_accessible(GOAL, FLUENTS) :-
    all_possible_states(FLUENTS, STATES_FROM),
    possibly_accessible_continue(STATES_FROM,[], GOAL),
    !.

possibly_accessible_continue([],_, _) :- !,fail.

possibly_accessible_continue([HEAD|_], _, GOAL) :-
    state(HEAD, FLUENTS),
    subset(GOAL, FLUENTS).

possibly_accessible_continue([HEAD|NOT_VISITED], VISITED, GOAL) :-
    state(HEAD, FLUENTS),
    not(subset(GOAL, FLUENTS)),
    findall([X,Y,Z,Z2], causes(X,Y,Z,Z2),R1),
    findall([X,Y,Z,Z2], typically_causes(X,Y,Z,Z2),R2),
    states_possible_with_causes(HEAD, R1, STATES1),
    states_possible_with_typically_causes(HEAD, R2, STATES2),
    append(STATES1,STATES2, STATES),
    subtract(STATES, [HEAD|VISITED], TO_BE_VISITED),
    subtract(TO_BE_VISITED, NOT_VISITED, TO_BE_VISITED2),
    append(NOT_VISITED, TO_BE_VISITED2, NOT_VISITED2),
    possibly_accessible_continue(NOT_VISITED2, [HEAD | VISITED], GOAL).


states_possible_with_causes(_, [],[]).

states_possible_with_causes(STATE_FROM, [HEAD|CAUSES], STATES_TO) :-
    states_possible_with_causes(STATE_FROM, CAUSES, STATES_TO2),
    nth0(0, HEAD, ACTION),
    nth0(1, HEAD, EXECUTOR),
    res0_trunc(ACTION, EXECUTOR, STATE_FROM,STATES_TO3),
    subtract(STATES_TO3, STATES_TO2, STATES_TO_ADD),
    append(STATES_TO2, STATES_TO_ADD, STATES_TO).

states_possible_with_typically_causes(_, [],[]).

states_possible_with_typically_causes(STATE_FROM, [HEAD|CAUSES], STATES_TO) :-
    states_possible_with_causes(STATE_FROM, CAUSES, STATES_TO2),
    nth0(0, HEAD, ACTION),
    nth0(1, HEAD, EXECUTOR),
    resN_trunc(ACTION, EXECUTOR, STATE_FROM,STATES_TO3),
    subtract(STATES_TO3, STATES_TO2, STATES_TO_ADD),
    append(STATES_TO2, STATES_TO_ADD, STATES_TO).


% typically_accessible(FLUENTS_TO, FLUENTS_FROM).

cart_prod(L1, L2, RES) :- bagof([X,Y],
    (member(X,L1),member(Y,L2)),RES).

prod([],[[]]).
prod([L|Ls],Out) :-
            bagof([X|R],(prod(Ls,O), member(X,L), member(R,O)),Out).

% possibly_involved(EXECUTOR, ACTIONS, EXECUTORS).
% always_involved(EXECUTOR, ACTIONS, EXECUTORS).
% typically_involved(EXECUTOR, ACTIONS, EXECUTORS).

% possibly(FLUENTS_TO, ACTIONS, EXECUTORS, FLUENTS_FROM).
% always(FLUENTS_TO, ACTIONS, EXECUTORS, FLUENTS_FROM).
% typically(FLUENTS_TO, ACTIONS, EXECUTORS, FLUENTS_FROM).

