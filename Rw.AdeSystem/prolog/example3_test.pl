:- [engine],[example3].

:- begin_tests(resy_example3).

test(turn1_hador_0) :-
    res0(turn1, hador, state0, [state1, state2]),
    res0_min(turn1, hador, state0, [state1]),
    res0_plus(turn1, hador, state0, [state1,state2]),
    resN(turn1,hador,state0,[state1]),
    resAb(turn1, hador, state0, []),
    resN_trunc(turn1,hador,state0,[state1]),
    resAb_trunc(turn1, hador, state0, []).

test(turn1_hador_1) :-
    res0(turn1, hador, state1, [state0, state3]),
    res0_min(turn1, hador, state1, [state0]),
    res0_plus(turn1, hador, state1, [state0,state3]),
    resN(turn1,hador,state1,[state0]),
    resAb(turn1, hador, state1, []),
    resN_trunc(turn1,hador,state1,[state0]),
    resAb_trunc(turn1, hador, state1, []).

test(turn1_hador_2) :-
    res0(turn1, hador, state2, [state0, state3]),
    res0_min(turn1, hador, state2, [state3]),
    res0_plus(turn1, hador, state2, [state0,state3]),
    resN(turn1,hador,state2,[state3]),
    resAb(turn1, hador, state2, []),
    resN_trunc(turn1,hador,state2,[state3]),
    resAb_trunc(turn1, hador, state2, []).

test(turn1_hador_3) :-
    res0(turn1, hador, state3, [state1, state2]),
    res0_min(turn1, hador, state3, [state2]),
    res0_plus(turn1, hador, state3, [state1,state2]),
    resN(turn1,hador,state3,[state2]),
    resAb(turn1, hador, state3, []),
    resN_trunc(turn1,hador,state3,[state2]),
    resAb_trunc(turn1, hador, state3, []).

test(turn1_mietus_0) :-
    res0(turn1, mietus, state0, [state0,state1, state2,state3]),
    res0_min(turn1, mietus, state0, [state0]),
    res0_plus(turn1, mietus, state0, [state1,state2]),
    resN(turn1,mietus,state0,[state1]),
    resAb(turn1, mietus, state0, [state0]),
    resN_trunc(turn1,mietus,state0,[state1]),
    resAb_trunc(turn1, mietus, state0, [state0]).

test(turn1_mietus_1) :-
    res0(turn1, mietus, state1, [state0, state1,state2,state3]),
    res0_min(turn1, mietus, state1, [state1]),
    res0_plus(turn1, mietus, state1, [state0,state3]),
    resN(turn1,mietus,state1,[state0]),
    resAb(turn1, mietus, state1, [state1]),
    resN_trunc(turn1,mietus,state1,[state0]),
    resAb_trunc(turn1, mietus, state1, [state1]).

test(turn1_mietus_2) :-
    res0(turn1, mietus, state2, [state0, state1,state2,state3]),
    res0_min(turn1, mietus, state2, [state2]),
    res0_plus(turn1, mietus, state2, [state0,state3]),
    resN(turn1,mietus,state2,[state3]),
    resAb(turn1, mietus, state2, [state2]),
    resN_trunc(turn1,mietus,state2,[state3]),
    resAb_trunc(turn1, mietus, state2, [state2]).

test(turn1_mietus_3) :-
    res0(turn1, mietus, state3, [state0,state1, state2,state3]),
    res0_min(turn1, mietus, state3, [state3]),
    res0_plus(turn1, mietus, state3, [state1,state2]),
    resN(turn1,mietus,state3,[state2]),
    resAb(turn1, mietus, state3, [state3]),
    resN_trunc(turn1,mietus,state3,[state2]),
    resAb_trunc(turn1, mietus, state3, [state3]).

test(turn2_hador_0) :-
    res0(turn2, hador, state0, [state0,state1, state2,state3]),
    res0_min(turn2, hador, state0, [state0]),
    res0_plus(turn2, hador, state0, [state2,state3]),
    resN(turn2,hador,state0,[state3]),
    resAb(turn2, hador, state0, [state0]),
    resN_trunc(turn2,hador,state0,[state3]),
    resAb_trunc(turn2, hador, state0, [state0]).

test(turn2_hador_1) :-
    res0(turn2, hador, state1, [state0, state1, state2, state3]),
    res0_min(turn2, hador, state1, [state1]),
    res0_plus(turn2, hador, state1, [state2,state3]),
    resN(turn2,hador,state1,[state2]),
    resAb(turn2, hador, state1, [state1]),
    resN_trunc(turn2,hador,state1,[state2]),
    resAb_trunc(turn2, hador, state1, [state1]).

test(turn2_hador_2) :-
    res0(turn2, hador, state2, [state0, state1]),
    res0_min(turn2, hador, state2, [state1]),
    res0_plus(turn2, hador, state2, [state0,state1]),
    resN(turn2,hador,state2,[state1]),
    resAb(turn2, hador, state2, []),
    resN_trunc(turn2,hador,state2,[state1]),
    resAb_trunc(turn2, hador, state2, []).

test(turn2_hador_3) :-
    res0(turn2, hador, state3, [state0, state1]),
    res0_min(turn2, hador, state3, [state0]),
    res0_plus(turn2, hador, state3, [state0,state1]),
    resN(turn2,hador,state3,[state0]),
    resAb(turn2, hador, state3, []),
    resN_trunc(turn2,hador,state3,[state0]),
    resAb_trunc(turn2, hador, state3, []).

test(turn2_mietus_0) :-
    res0(turn2, mietus, state0, [state2,state3]),
    res0_min(turn2, mietus, state0, [state3]),
    res0_plus(turn2, mietus, state0, [state2,state3]),
    resN(turn2,mietus,state0,[state3]),
    resAb(turn2, mietus, state0, []),
    resN_trunc(turn2,mietus,state0,[state3]),
    resAb_trunc(turn2, mietus, state0, []).

test(turn2_mietus_1) :-
    res0(turn2, mietus, state1, [state2,state3]),
    res0_min(turn2, mietus, state1, [state2]),
    res0_plus(turn2, mietus, state1, [state2,state3]),
    resN(turn2,mietus,state1,[state2]),
    resAb(turn2, mietus, state1, [state0]),
    resN_trunc(turn2,mietus,state1,[state2]),
    resAb_trunc(turn2, mietus, state1, [state0]).

test(turn2_mietus_2) :-
    res0(turn2, mietus, state2, [state0, state1,state2,state3]),
    res0_min(turn2, mietus, state2, [state2]),
    res0_plus(turn2, mietus, state2, [state0,state1]),
    resN(turn2,mietus,state2,[state1]),
    resAb(turn2, mietus, state2, [state2]),
    resN_trunc(turn2,mietus,state2,[state1]),
    resAb_trunc(turn2, mietus, state2, [state2]).

test(turn2_mietus_3) :-
    res0(turn2, mietus, state3, [state0,state1, state2,state3]),
    res0_min(turn2, mietus, state3, [state3]),
    res0_plus(turn2, mietus, state3, [state0,state1]),
    resN(turn2,mietus,state3,[state0]),
    resAb(turn2, mietus, state3, [state3]),
    resN_trunc(turn2,mietus,state3,[state0]),
    resAb_trunc(turn2, mietus, state3, [state3]).


:- end_tests(resy_example3).
