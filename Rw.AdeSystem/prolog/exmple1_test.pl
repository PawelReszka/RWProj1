:- [states],[exmple1].

:- begin_tests(resy).

test(chown_hador_0) :-
    res0(chown, hador, state0, [state1, state3, state5, state7]),
    res0_min(chown, hador, state0, [state1]),
    res0_plus(chown, hador, state0, [state1, state3, state5, state7]),
    resN(chown, hador, state0, [state1]),
    resAb(chown, hador, state0, []).

test(chown_mietus_0) :-
    res0(chown, mietus, state0, [state0, state2, state4, state6]),
    res0_min(chown, mietus, state0, [state0]),
    res0_plus(chown, mietus, state0, [state0, state2, state4, state6]),
    resN(chown, mietus, state0, [state0]),
    resAb(chown, mietus, state0, []).

test(load_epsilon_0) :-
    res0(load, epsilon, state0, [state2, state3, state6, state7]),
    res0_min(load, epsilon, state0, [state2]),
    res0_plus(load, epsilon, state0, [state2, state3, state6, state7]),
    resN(load, epsilon, state0, [state2]),
    resAb(load, epsilon, state0, []).

test(load_epsilon_2) :-
    res0(load, epsilon, state2, [state2, state3, state6, state7]),
    res0_min(load, epsilon, state2, [state2]),
    res0_plus(load, epsilon, state2, [state2, state3, state6, state7]),
    resN(load, epsilon, state2, [state2]),
    resAb(load, epsilon, state2, []).

test(shoot_hador_0) :-
    res0(shoot, hador, state0, [state0, state1, state4, state5]),
    res0_min(shoot, hador, state0, [state0]),
    res0_plus(shoot, hador, state0, [state0, state1, state4, state5]),
    resN(shoot, hador, state0, [state0]),
    resAb(shoot, hador, state0, []).

test(shoot_hador_1) :-
    res0(shoot, hador, state1, [state0, state1, state2, state3, state4,state5, state6, state7]),
    res0_min(shoot, hador, state1, [state1]),
    res0_plus(shoot, hador, state1, [state0, state1, state2, state3, state4, state5, state6, state7]),
    resN(shoot, hador, state1, [state1]),
    resAb(shoot, hador, state1, []).

test(shoot_hador_2) :-
    res0(shoot, hador, state2, [state0, state1, state4, state5]),
    res0_min(shoot, hador, state2, [state0]),
    res0_plus(shoot, hador, state2, [state4, state5]),
    resN(shoot, hador, state2, [state4]),
    resAb(shoot, hador, state2, [state0]).

test(shoot_mietus_2) :-
    res0(shoot, mietus, state2, [state0, state1, state2, state3,state4, state5,state6,state7]),
    res0_min(shoot, mietus, state2, [state2]),
    res0_plus(shoot, mietus, state2, [state0,state1,state2,state3,state4, state5,state6,state7]),
    resN(shoot, mietus, state2, [state2]),
    resAb(shoot, mietus, state2, []).

test(shoot_mietus_3) :-
    res0(shoot, mietus, state3, [state4, state5]),
    res0_min(shoot, mietus, state3, [state5]),
    res0_plus(shoot, mietus, state3, [state4,state5]),
    resN(shoot, mietus, state3, [state5]),
    resAb(shoot, mietus, state3, []).

test(shoot_hador_6) :-
    res0(shoot, hador, state6, [state0, state1, state4, state5]),
    res0_min(shoot, hador, state6, [state4]),
    res0_plus(shoot, hador, state6, [state4,state5]),
    resN(shoot, hador, state6, [state4]),
    resAb(shoot, hador, state6, []).

test(shoot_mietus_0) :-
    res0(shoot, mietus, state0, [state0,state1,state2,state3,state4, state5,state6,state7]),
    res0_min(shoot, mietus, state0, [state0]),
    res0_plus(shoot, mietus, state0, [state0,state1,state2,state3,state4,state5,state6,state7]),
    resN(shoot, mietus, state0, [state0]),
    resAb(shoot, mietus, state0, []).

test(shoot_mietus_1) :-
    res0(shoot, mietus, state1, [state0,state1,state4,state5]),
    res0_min(shoot, mietus, state1, [state1]),
    res0_plus(shoot, mietus, state1, [state0,state1,state4,state5]),
    resN(shoot, mietus, state1, [state1]),
    resAb(shoot, mietus, state1, []).

test(shoot_hador_7) :-
    res0(shoot, hador, state7, [state0, state1, state2,state3,state4, state5,state6,state7]),
    res0_min(shoot, hador, state7, [state7]),
    res0_plus(shoot, hador, state7, [state0,state1,state2,state3,state4,state5,state6,state7]),
    resN(shoot, hador, state7, [state7]),
    resAb(shoot, hador, state7, []).

test(shoot_hador_4) :-
    res0(shoot, hador, state4, [state0,state1,state4,state5]),
    res0_min(shoot, hador, state4, [state4]),
    res0_plus(shoot, hador, state4, [state0,state1,state4,state5]),
    resN(shoot, hador, state4, [state4]),
    resAb(shoot, hador, state4, []).


:- end_tests(resy).
