fluent(has_gun_mietus).
fluent(has_gun_hador).
fluent(loaded).
fluent(alive).

action(load).
action(shoot).
action(chown).

executor(hador).
executor(mietus).


sneg(has_gun_mietus, not_has_gun_mietus).
sneg(has_gun_hador, not_has_gun_hador).
sneg(loaded, not_loaded).
sneg(alive, not_alive).

neg(X,Y) :- sneg(Y,X).
neg(X,Y) :- sneg(X,Y).

neg(X,X) :- !,fail.

sinertial(has_gun_mietus).
sinertial(has_gun_hador).
sinertial(loaded).
sinertial(chown).

inertial(X) :- sinertial(X).
inertial(X) :- neg(X,Y),
               sinertial(Y).

noninertial(X) :- not(inertial(X)).

initially([hador_ma_bron, not_has_gun_mietus, alive]).

state(state0, [has_gun_hador, not_has_gun_mietus, not_loaded, alive]).
state(state1, [not_has_gun_hador, has_gun_mietus, not_loaded, alive]).
state(state2, [has_gun_hador, not_has_gun_mietus, loaded, alive]). 
state(state3, [not_has_gun_hador, has_gun_mietus, loaded, alive]).
state(state4, [has_gun_hador, not_has_gun_mietus, not_loaded, not_alive]).
state(state5, [not_has_gun_hador, has_gun_mietus, not_loaded, not_alive]).
state(state6, [has_gun_hador, not_has_gun_mietus, loaded, not_alive]).
state(state7, [not_has_gun_hador, has_gun_mietus, loaded, not_alive]).

releases(chown, hador, [bron_naladowana], []).

causes(chown, hador, [has_gun_mietus, not_has_gun_hador], []).
causes(chown, mietus, [has_gun_hador, not_has_gun_mietus], []).
causes(load, epsilon, [loaded], []).

causes(shoot, mietus, [not_loaded], [has_gun_mietus]).
causes(shoot, hador, [not_loaded], [has_gun_hador]).

causes(shoot, mietus, [not_alive], [loaded, has_gun_mietus]).
typically_causes(shoot, hador, [not_alive], [loaded, has_gun_hador]).

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
    findall([X,Y], causes(ACTION, epsilon, X, Y), R2),
    append(R1,R2,R),
    length(R,R_LENGTH),
    R_LENGTH > 0,
    state(STATE1, STATE1_LIST),
    state(STATE2, STATE2_LIST),
    possible_causes_fto_states2(R, STATE1_LIST, STATE2_LIST).

possible_typically_causes_fto_states(ACTION, EXECUTOR, STATE1, STATE2) :-
    findall([X,Y], typically_causes(ACTION, EXECUTOR, X, Y), R),
    length(R,R_LENGTH),
    R_LENGTH == 0,
    findall([X,Y], causes(ACTION, EXECUTOR, X, Y), R2),
    state(STATE1, STATE1_LIST),
    state(STATE2, STATE2_LIST),
    possible_causes_fto_states2(R2, STATE1_LIST, STATE2_LIST).

possible_causes_fto_states2([], _, _).
possible_causes_fto_states2([HEAD|TAIL],LIST1, LIST2) :-
    nth0(0, HEAD, HEAD1),
    nth0(1, HEAD, HEAD2),
    (not(subset(HEAD2, LIST2)); subset(HEAD1, LIST1)),
    possible_causes_fto_states2(TAIL, LIST1, LIST2).

res0(ACTION, EXECUTOR, STATE, STATES) :-
    list_of_states(ALL),
    res0_continue(ACTION, EXECUTOR, STATE, ALL, STATES).

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
    res0_plus_continue(ACTION, EXECUTOR, STATE, ALL, STATES).

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
    subtract(LIST1, LIST2, OUTPUT).

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
   copy_res0_state_if_minimal_new(STATES_0, STATE, MINIMAL, STATES),
   !.

resN(ACTION, EXECUTOR, STATE, STATES) :-
   res0_plus(ACTION, EXECUTOR,STATE, STATES_0),
   minimal_length_new_of_res0(STATES_0, STATE, MINIMAL),
   copy_res0_state_if_minimal_new(STATES_0, STATE, MINIMAL, STATES),
   !.

resAb(ACTION,EXECUTOR, STATE, STATES) :-
    res0_min(ACTION, EXECUTOR, STATE, STATES0),
    resN(ACTION, EXECUTOR, STATE, STATESN),
    subtract(STATES0, STATESN, STATES),
    !.

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

% possibly_accessible(STATE_TO, STATE_FROM).
% always_accessible(STATE_TO, STATE_FROM).
% typically_accessible(STATE_TO, STATE_FROM).

% possibly_involved(EXECUTOR, ACTIONS, EXECUTORS).
% always_involved(EXECUTOR, ACTIONS, EXECUTORS).
% typically_involved(EXECUTOR, ACTIONS, EXECUTORS).

% possibly(FLUENTS_TO, ACTIONS, EXECUTORS, FLUENTS_FROM).
% always(FLUENTS_TO, ACTIONS, EXECUTORS, FLUENTS_FROM).
% typically(FLUENTS_TO, ACTIONS, EXECUTORS, FLUENTS_FROM).

