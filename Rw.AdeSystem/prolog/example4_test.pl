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

:- end_tests(resy_example4).
