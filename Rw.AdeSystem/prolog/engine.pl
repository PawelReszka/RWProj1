:-  dynamic(releases/4),dynamic(preserve/3).

neg(X,Y) :- sneg(Y,X).
neg(X,Y) :- sneg(X,Y).

neg(X,X) :- !,fail.

inertial(X) :- sinertial(X).
inertial(X) :- neg(X,Y),
               sinertial(Y).

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
    join_possible_states(OUTPUT2, [STATE1_LIST, STATE2_LIST], OUTPUT),
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
    findall([X], always(X), R),
    state_valid_continue(R, X).

state_valid_continue([HEAD|_], STATE) :-
    nth0(0, HEAD, FORMULA),
    state_valid_with_formula(STATE, FORMULA).

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

cause_change(LIST, STATE2, STATE1) :-
    state(STATE1, STATE1_LIST),
    force_cause_change(LIST, STATE1_LIST, STATE2_LIST),
    state(STATE2, STATE2_LIST),
    !.

force_cause_change(_,[],[]).

force_cause_change(LIST,[TOP1|X],[TOP2|Y]) :-
    neg(TOP1,TOP2),
    member(TOP2,LIST),
    force_cause_change(LIST,X,Y).

force_cause_change(LIST,[TOP1|X],[TOP1|Y]) :-
    neg(TOP1,TOP2),
    not(member(TOP2,LIST)),
    force_cause_change(LIST,X,Y).

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

res0(ACTION, EXECUTOR, STATE, STATES) :-
    list_of_states(ALL),
    res0_continue(ACTION, EXECUTOR, STATE, ALL, STATES2),
    released_fluents(ACTION, EXECUTOR, STATE, FLUENTS),
    release_fluents(STATES2, FLUENTS, STATES3),
    sort(STATES3,STATES),
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

res0_plus(ACTION, EXECUTOR, STATE, STATES) :-
    res0(ACTION, EXECUTOR, STATE, ALL),
    res0_plus_continue(ACTION, EXECUTOR, STATE, ALL, STATES2),
    released_fluents(ACTION, EXECUTOR, STATE, FLUENTS),
    release_fluents(STATES2, FLUENTS, STATES3),
    sort(STATES3,STATES),
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

new(STATE1, STATE2,OUTPUT) :-
    state(STATE1, LIST1),
    state(STATE2, LIST2),
    subtract(LIST1, LIST2, OUTPUT2),
    findall(X,fluent(X),FLUENTS),
    findall(Y,sinertial(Y),INERTIAL),
    subtract(FLUENTS, INERTIAL, NONINERTIAL2),
    convert_negatives(NONINERTIAL2, NEGNONINERTIAL2),
    append(NONINERTIAL2, NEGNONINERTIAL2, NONINERTIAL),
    subtract(OUTPUT2, NONINERTIAL, OUTPUT).

minimal_length_new_of_res0([X], STATE, Y) :-
    new(X, STATE, OUTPUT),
    length(OUTPUT,Y).

minimal_length_new_of_res0([HEAD|LIST], STATE, LENGTH) :-
    new(HEAD, STATE, OUTPUT),
    length(OUTPUT, OUTPUT_LENGTH),
    minimal_length_new_of_res0(LIST, STATE, OUTPUT_LENGTH2),
    (
        OUTPUT_LENGTH < OUTPUT_LENGTH2, LENGTH is OUTPUT_LENGTH ;
        OUTPUT_LENGTH >= OUTPUT_LENGTH2, LENGTH is OUTPUT_LENGTH2
    ).

copy_res0_state_if_minimal_new([],_,_,[]).

copy_res0_state_if_minimal_new([HEAD|LIST], STATE, MINIMAL, [HEAD|OUTPUT]) :-
    new(HEAD, STATE, OUTPUT1),
    length(OUTPUT1, OUTPUT_LENGTH),
    MINIMAL == OUTPUT_LENGTH,
    copy_res0_state_if_minimal_new(LIST, STATE, MINIMAL, OUTPUT).

copy_res0_state_if_minimal_new([HEAD|LIST], STATE, MINIMAL, OUTPUT) :-
    new(HEAD, STATE, OUTPUT1),
    length(OUTPUT1, OUTPUT_LENGTH),
    MINIMAL < OUTPUT_LENGTH,
    copy_res0_state_if_minimal_new(LIST, STATE, MINIMAL, OUTPUT).


res0_min(ACTION, EXECUTOR, STATE, STATES) :-
   res0(ACTION, EXECUTOR,STATE, STATES_0),
   minimal_length_new_of_res0(STATES_0, STATE, MINIMAL),
   copy_res0_state_if_minimal_new(STATES_0, STATE, MINIMAL, STATES2),
   released_fluents(ACTION, EXECUTOR, STATE, FLUENTS),
   release_fluents(STATES2, FLUENTS, STATES3),
   sort(STATES3,STATES),
   !.

resN(ACTION, EXECUTOR, STATE, STATES) :-
   res0_plus(ACTION, EXECUTOR,STATE, STATES_0),
   minimal_length_new_of_res0(STATES_0, STATE, MINIMAL),
   copy_res0_state_if_minimal_new(STATES_0, STATE, MINIMAL, STATES2),
   released_fluents(ACTION, EXECUTOR, STATE, FLUENTS),
   release_fluents(STATES2, FLUENTS, STATES3),
   sort(STATES3,STATES),
   !.


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
    res0_min(ACTION, EXECUTOR, STATE, X),
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

accessible(STATE_TO, STATE_FROM) :-
    accessible_continue([STATE_FROM],[], STATE_TO).

accessible_continue([],_, _) :- !,fail.

accessible_continue([HEAD|_], _, GOAL) :-
    state(HEAD, FLUENTS),
    subset(GOAL, FLUENTS).

accessible_continue([HEAD|NOT_VISITED], VISITED, GOAL) :-
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
    accessible_continue(NOT_VISITED2, [HEAD | VISITED], GOAL).

typically_accessible(STATE_TO, STATE_FROM) :-
    typically_accessible_continue([STATE_FROM],[], STATE_TO).

typically_accessible_continue([],_, _) :- !,fail.

typically_accessible_continue([HEAD|_], _, GOAL) :-
    state(HEAD, FLUENTS),
    subset(GOAL, FLUENTS).

typically_accessible_continue([HEAD|NOT_VISITED], VISITED, GOAL) :-
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
    typically_accessible_continue(NOT_VISITED2, [HEAD | VISITED], GOAL).


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


possibly_accessible(FLUENTS_TO, FLUENTS_FROM) :-
    possible_states(FLUENTS_FROM, STATES),
    possible_states_continue(STATES, FLUENTS_TO).

possibly_accessible_continue([], _) :- !,fail.

possibly_accessible_continue([HEAD|_], FLUENTS_TO) :-
    accessible(FLUENTS_TO, HEAD).

possibly_accessible_continue([HEAD|STATES], FLUENTS_TO) :-
    not(accessible(FLUENTS_TO, HEAD)),
    possibly_accessible_continue(STATES, FLUENTS_TO).

always_accessible(FLUENTS_TO, FLUENTS_FROM) :-
    possible_states(FLUENTS_FROM, STATES),
    always_accessible_continue(STATES, FLUENTS_TO).

always_accessible_continue([], _).

always_accessible_continue([HEAD|STATES], FLUENTS_TO) :-
    accessible(FLUENTS_TO, HEAD),
    always_accessible_continue(STATES, FLUENTS_TO).

% typically_accessible(FLUENTS_TO, FLUENTS_FROM).

% possibly_involved(EXECUTOR, ACTIONS, EXECUTORS).
% always_involved(EXECUTOR, ACTIONS, EXECUTORS).
% typically_involved(EXECUTOR, ACTIONS, EXECUTORS).

% possibly(FLUENTS_TO, ACTIONS, EXECUTORS, FLUENTS_FROM).
% always(FLUENTS_TO, ACTIONS, EXECUTORS, FLUENTS_FROM).
% typically(FLUENTS_TO, ACTIONS, EXECUTORS, FLUENTS_FROM).

