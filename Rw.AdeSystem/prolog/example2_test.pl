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

test(entice_mietus_1) :-
    res0(entice, mietus, state1, [state0, state1,state2,state3,state4,state5]),
    res0_min(entice,mietus, state1, [state1]),
    res0_plus(entice, mietus, state1, [state0, state1]),
    resN(entice, mietus, state1, [state1]),
    resAb(entice, mietus, state1, []),
    resN_trunc(entice, mietus, state1, [state1]),
    resAb_trunc(entice, mietus, state1, []).

test(entice_mietus_2) :-
    res0(entice, mietus, state2, [state0, state1,state2,state3,state4,state5]),
    res0_min(entice,mietus, state2, [state2]),
    res0_plus(entice, mietus, state2, [state0, state1]),
    resN(entice, mietus, state2, [state0]),
    resAb(entice, mietus, state2, [state2]),
    resN_trunc(entice, mietus, state2, [state0]),
    resAb_trunc(entice, mietus, state2, [state2]).

test(entice_mietus_3) :-
    res0(entice, mietus, state3, [state0, state1,state2,state3,state4,state5]),
    res0_min(entice,mietus, state3, [state3]),
    res0_plus(entice, mietus, state3, [state0, state1]),
    resN(entice, mietus, state3, [state1]),
    resAb(entice, mietus, state3, [state3]),
    resN_trunc(entice, mietus, state3, [state1]),
    resAb_trunc(entice, mietus, state3, [state3]).

test(entice_mietus_4) :-
    res0(entice, mietus, state4, [state0, state1,state2,state3,state4,state5]),
    res0_min(entice,mietus, state4, [state4]),
    res0_plus(entice, mietus, state4, [state0, state1]),
    resN(entice, mietus, state4, [state0]),
    resAb(entice, mietus, state4, [state4]),
    resN_trunc(entice, mietus, state4, [state0]),
    resAb_trunc(entice, mietus, state4, [state4]).

test(entice_mietus_5) :-
    res0(entice, mietus, state5, [state0, state1,state2,state3,state4,state5]),
    res0_min(entice,mietus, state5, [state5]),
    res0_plus(entice, mietus, state5, [state0, state1]),
    resN(entice, mietus, state5, [state1]),
    resAb(entice, mietus, state5, [state5]),
    resN_trunc(entice, mietus, state5, [state1]),
    resAb_trunc(entice, mietus, state5, [state5]).

test(shoot_hador_0) :-
    resN(shoot,hador,state0,[state4]),
    resAb(shoot,hador,state0,[state0]).

test(shoot_hador_1) :-
    resN(shoot,hador,state1,[state1]),
    resAb(shoot,hador,state1,[]).

test(shoot_hador_2) :-
    resN(shoot,hador,state2,[state4]),
    resAb(shoot,hador,state2,[state2]).

test(shoot_hador_3) :-
    resN(shoot,hador,state3,[state3]),
    resAb(shoot,hador,state3,[]).

test(shoot_hador_4) :-
    resN(shoot,hador,state4,[state4]),
    resAb(shoot,hador,state4,[]).

test(shoot_hador_5) :-
    resN(shoot,hador,state5,[state5]),
    resAb(shoot,hador,state5,[]).

test(shoot_mietus_0) :-
    resN(shoot,mietus,state0,[state0]),
    resAb(shoot,mietus,state0,[]).

test(shoot_mietus_1) :-
    resN(shoot,mietus,state1,[state5]),
    resAb(shoot,mietus,state1,[]).

test(shoot_mietus_2) :-
    resN(shoot,mietus,state2,[state2]),
    resAb(shoot,mietus,state2,[]).

test(shoot_mietus_3) :-
    resN(shoot,mietus,state3,[state5]),
    resAb(shoot,mietus,state3,[]).

test(shoot_mietus_4) :-
    resN(shoot,mietus,state4,[state4]),
    resAb(shoot,mietus,state4,[]).

test(shoot_mietus_5) :-
    resN(shoot,mietus,state5,[state5]),
    resAb(shoot,mietus,state5,[]).





:- end_tests(resy_example2).
