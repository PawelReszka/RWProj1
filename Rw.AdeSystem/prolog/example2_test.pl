:- [engine],[example2].

:- begin_tests(resy_example2).

test(entice_hador_0) :-
    res0(entice, hador, state0, [state0, state1]),
    res0_min(entice,hador, state0, [state0]),
    res0_plus(entice, hador, state0, [state0, state1]),
    resN(entice, hador, state0, [state0]),
    resAb(entice, hador, state0, []),
    resN_trunc(entice, hador, state0, [state0]),
    resAb_trunc(entice, hador, state0, []).

test(entice_hador_1) :-
    res0(entice, hador, state1, [state0, state1]),
    res0_min(entice,hador, state1, [state1]),
    res0_plus(entice, hador, state1, [state0, state1]),
    resN(entice, hador, state1, [state1]),
    resAb(entice, hador, state1, []),
    resN_trunc(entice, hador, state1, [state1]),
    resAb_trunc(entice, hador, state1, []).

test(entice_hador_2) :-
    res0(entice, hador, state2, [state0, state1]),
    res0_min(entice,hador, state2, [state0]),
    res0_plus(entice, hador, state2, [state0, state1]),
    resN(entice, hador, state2, [state0]),
    resAb(entice, hador, state2, []),
    resN_trunc(entice, hador, state2, [state0]),
    resAb_trunc(entice, hador, state2, []).

test(entice_hador_3) :-
    res0(entice, hador, state3, [state0, state1]),
    res0_min(entice,hador, state3, [state1]),
    res0_plus(entice, hador, state3, [state0, state1]),
    resN(entice, hador, state3, [state1]),
    resAb(entice, hador, state3, []),
    resN_trunc(entice, hador, state3, [state1]),
    resAb_trunc(entice, hador, state3, []).

test(entice_hador_4) :-
    res0(entice, hador, state4, [state0, state1]),
    res0_min(entice,hador, state4, [state0]),
    res0_plus(entice, hador, state4, [state0, state1]),
    resN(entice, hador, state4, [state0]),
    resAb(entice, hador, state4, []),
    resN_trunc(entice, hador, state4, []),
    resAb_trunc(entice, hador, state4, []).

test(entice_hador_5) :-
    res0(entice, hador, state5, [state0, state1]),
    res0_min(entice,hador, state5, [state1]),
    res0_plus(entice, hador, state5, [state0, state1]),
    resN(entice, hador, state5, [state1]),
    resAb(entice, hador, state5, []),
    resN_trunc(entice, hador, state5, []),
    resAb_trunc(entice, hador, state5, []).

test(entice_mietus_0) :-
    res0(entice, mietus, state0, [state0, state1,state2,state3,state4,state5]),
    res0_min(entice,mietus, state0, [state0]),
    res0_plus(entice, mietus, state0, [state0, state1]),
    resN(entice, mietus, state0, [state0]),
    resAb(entice, mietus, state0, []),
    resN_trunc(entice, mietus, state0, [state0]),
    resAb_trunc(entice, mietus, state0, []).


:- end_tests(resy_example2).
