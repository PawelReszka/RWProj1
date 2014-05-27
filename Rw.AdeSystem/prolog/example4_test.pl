:- [engine],[example4].

:- begin_tests(resy_example4).

test(chown_mietus_0) :-
    res0(chown, mietus, state0, [state0, state2,state4]),
    res0_min(chown,mietus, state0, [state0]),
    res0_plus(chown, mietus, state0, [state0, state2,state4]),
    resN(chown, mietus, state0, [state0]),
    resAb(chown, mietus, state0, []),
    resN_trunc(chown, mietus, state0, [state0]),
    resAb_trunc(chown, mietus, state0, []).

test(chown_hador_0) :-
    res0(chown, hador, state0, [state1, state3,state5]),
    res0_min(chown,hador, state0, [state1]),
    res0_plus(chown, hador, state0, [state1, state3,state5]),
    resN(chown, hador, state0, [state1]),
    resAb(chown, hador, state0, []),
    resN_trunc(chown, hador, state0, [state1]),
    resAb_trunc(chown, hador, state0, []).

test(shoot_mietus_0) :-
    res0(shoot, mietus, state0, [state0, state1,state2,state3,state4,state5]),
    res0_min(shoot,mietus, state0, [state0]),
    res0_plus(shoot, mietus, state0, [state0,state1,state2,state3,state4,state5 ]),
    resN(shoot, mietus, state0, [state0]),
    resAb(shoot, mietus, state0, []),
    resN_trunc(shoot, mietus, state0, [state0]),
    resAb_trunc(shoot, mietus, state0, []).

test(shoot_hador_0) :-
    res0(shoot, hador, state0, [state0, state1,state2,state3,state4,state5]),
    res0_min(shoot,hador, state0, [state0,state2]),
    res0_plus(shoot, hador, state0, [state4,state5 ]),
    resN(shoot, hador, state0, [state4]),
    resAb(shoot, hador, state0, [state0,state2]),
    resN_trunc(shoot, hador, state0, [state4]),
    resAb_trunc(shoot, hador, state0, [state0,state2]).

test(shoot_mietus_1) :-
    res0(shoot, mietus, state1, [state4,state5]),
    res0_min(shoot,mietus, state1, [state5]),
    res0_plus(shoot, mietus, state1, [state4,state5 ]),
    resN(shoot, mietus, state1, [state5]),
    resAb(shoot, mietus, state1, []),
    resN_trunc(shoot, mietus, state1, [state5]),
    resAb_trunc(shoot, mietus, state1, []).

test(shoot_hador_1) :-
    res0(shoot, hador, state1, [state0, state1,state2,state3,state4,state5]),
    res0_min(shoot,hador, state1, [state1]),
    res0_plus(shoot, hador, state1, [state0,state1,state2,state3,state4,state5]),
    resN(shoot, hador, state1, [state1]),
    resAb(shoot, hador, state1, []),
    resN_trunc(shoot, hador, state1, [state1]),
    resAb_trunc(shoot, hador, state1, []).

test(shoot_mietus_2) :-
    res0(shoot, mietus, state2, [state0, state1,state2,state3,state4,state5]),
    res0_min(shoot,mietus, state2, [state2]),
    res0_plus(shoot, mietus, state2, [state0,state1,state2,state3,state4,state5]),
    resN(shoot, mietus, state2, [state2]),
    resAb(shoot, mietus, state2, []),
    resN_trunc(shoot, mietus, state2, [state2]),
    resAb_trunc(shoot, mietus, state2, []).

test(shoot_hador_2) :-
    res0(shoot, hador, state2, [state0, state1,state2,state3,state4,state5]),
    res0_min(shoot,hador, state2, [state0,state2]),
    res0_plus(shoot, hador, state2, [state4,state5]),
    resN(shoot, hador, state2, [state4]),
    resAb(shoot, hador, state2, [state0,state2]),
    resN_trunc(shoot, hador, state2, [state4]),
    resAb_trunc(shoot, hador, state2, [state0,state2]).

test(shoot_mietus_3) :-
    res0(shoot, mietus, state3, [state4,state5]),
    res0_min(shoot,mietus, state3, [state5]),
    res0_plus(shoot, mietus, state3, [state4,state5]),
    resN(shoot, mietus, state3, [state5]),
    resAb(shoot, mietus, state3, []),
    resN_trunc(shoot, mietus, state3, [state5]),
    resAb_trunc(shoot, mietus, state3, []).

test(shoot_hador_3) :-
    res0(shoot, hador, state3, [state0, state1,state2,state3,state4,state5]),
    res0_min(shoot,hador, state3, [state3]),
    res0_plus(shoot, hador, state3, [state0,state1,state2,state3,state4,state5]),
    resN(shoot, hador, state3, [state3]),
    resAb(shoot, hador, state3, []),
    resN_trunc(shoot, hador, state3, [state3]),
    resAb_trunc(shoot, hador, state3, []).

test(shoot_mietus_4) :-
    res0(shoot, mietus, state4, [state0,state1,state2,state3,state4,state5]),
    res0_min(shoot,mietus, state4, [state4]),
   res0_plus(shoot, mietus, state4, [state0,state1,state2,state3,state4,state5]),
    resN(shoot, mietus, state4, [state4]),
    resAb(shoot, mietus, state4, []),
    resN_trunc(shoot, mietus, state4, [state4]),
    resAb_trunc(shoot, mietus, state4, []).

test(shoot_hador_4) :-
    res0(shoot, hador, state4, [state0, state1,state2,state3,state4,state5]),
    res0_min(shoot,hador, state4, [state0,state4]), % state4 ma na pewno 0 różnicy, a takich jest tylko 1
    res0_plus(shoot, hador, state4, [state4,state5]),
    resN(shoot, hador, state4, [state4]),
    resAb(shoot, hador, state4, [state0]),
    resN_trunc(shoot, hador, state4, [state4]),
    resAb_trunc(shoot, hador, state4, [state0]).

test(shoot_mietus_5) :-
    res0(shoot, mietus, state5, [state4,state5]),
    res0_min(shoot,mietus, state5, [state5]),
   res0_plus(shoot, mietus, state5, [state4,state5]),
    resN(shoot, mietus, state5, [state5]),
    resAb(shoot, mietus, state5, []),
    resN_trunc(shoot, mietus, state5, [state5]),
    resAb_trunc(shoot, mietus, state5, []).

test(shoot_hador_5) :-
    res0(shoot, hador, state5, [state0,state1,state2,state3,state4,state5]),
    res0_min(shoot,hador, state5, [state5]),
    res0_plus(shoot, hador, state5, [state0,state1,state2,state3,state4,state5]),
    resN(shoot, hador, state5, [state5]),
    resAb(shoot, hador, state5, []),
    resN_trunc(shoot, hador, state5, [state5]),
    resAb_trunc(shoot, hador, state5, []).


:- end_tests(resy_example4).
