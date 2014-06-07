:- [engine],[example1].

:- begin_tests(resy).

test(chown_hador_0) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state5, STATE5),
    state(state7, STATE7),

    sort([STATE1, STATE3, STATE5, STATE7],L1),
    res0(chown, hador, STATE0, L1),
    res0_min(chown, hador, STATE0, [STATE1]),
    sort([STATE1, STATE3, STATE5, STATE7],L2),
    res0_plus(chown, hador, STATE0,L2),
    resN(chown, hador, STATE0, [STATE1]),
    resAb(chown, hador, STATE0, []).

test(chown_mietus_0) :-
    state(state0, STATE0),
    state(state2, STATE2),
    state(state4, STATE4),
    state(state6, STATE6),

    sort([STATE0, STATE2, STATE4, STATE6],L1),
    res0(chown, mietus, STATE0, L1),
    res0_min(chown, mietus, STATE0, [STATE0]),
    sort([STATE0, STATE2, STATE4, STATE6],L2),
    res0_plus(chown, mietus, STATE0, L2),
    resN(chown, mietus, STATE0, [STATE0]),
    resAb(chown, mietus, STATE0, []).

test(load_epsilon_0) :-
    state(state0, STATE0),
    state(state3, STATE3),
    state(state7, STATE7),
    state(state2, STATE2),
    state(state6, STATE6),

    sort([STATE2, STATE3, STATE6, STATE7],L1),
    res0(load, epsilon, STATE0, L1),
    res0_min(load, epsilon, STATE0, [STATE2]),
    sort([STATE2, STATE3, STATE6, STATE7],L2),
    res0_plus(load, epsilon, STATE0, L2),
    resN(load, epsilon, STATE0, [STATE2]),
    resAb(load, epsilon, STATE0, []).

test(load_epsilon_2) :-
    state(state3, STATE3),
    state(state7, STATE7),
    state(state2, STATE2),
    state(state6, STATE6),

    sort([STATE2, STATE3, STATE6, STATE7],L1),
    res0(load, epsilon, STATE2, L1),
    res0_min(load, epsilon, STATE2, [STATE2]),
    sort([STATE2, STATE3, STATE6, STATE7],L2),
    res0_plus(load, epsilon, STATE2, L2),
    resN(load, epsilon, STATE2, [STATE2]),
    resAb(load, epsilon, STATE2, []).

test(shoot_hador_0) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state5, STATE5),
    state(state4, STATE4),

    sort([STATE0, STATE1, STATE4, STATE5],L1),
    res0(shoot, hador, STATE0, L1),
    res0_min(shoot, hador, STATE0, [STATE0]),
    sort([STATE0, STATE1, STATE4, STATE5],L2),
    res0_plus(shoot, hador, STATE0, L2),
    resN(shoot, hador, STATE0, [STATE0]),
    resAb(shoot, hador, STATE0, []).

test(shoot_hador_1) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state5, STATE5),
    state(state7, STATE7),
    state(state2, STATE2),
    state(state4, STATE4),
    state(state6, STATE6),

    sort([STATE0, STATE1, STATE2, STATE3, STATE4,STATE5, STATE6, STATE7],L1),
    res0(shoot, hador, STATE1, L1),
    res0_min(shoot, hador, STATE1, [STATE1]),
    sort([STATE0, STATE1, STATE2, STATE3, STATE4, STATE5, STATE6, STATE7],L2),
    res0_plus(shoot, hador, STATE1, L2),
    resN(shoot, hador, STATE1, [STATE1]),
    resAb(shoot, hador, STATE1, []).

test(shoot_hador_2) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state5, STATE5),
    state(state2, STATE2),
    state(state4, STATE4),
    sort([STATE0, STATE1, STATE4, STATE5],L1),
    sort([STATE4, STATE5],L2),
    res0(shoot, hador, STATE2, L1),
    res0_min(shoot, hador, STATE2, [STATE0]),
    res0_plus(shoot, hador, STATE2, L2),
    resN(shoot, hador, STATE2, [STATE4]),
    resAb(shoot, hador, STATE2, [STATE0]).

test(shoot_mietus_2) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state5, STATE5),
    state(state7, STATE7),
    state(state2, STATE2),
    state(state4, STATE4),
    state(state6, STATE6),

    sort([STATE0, STATE1, STATE2, STATE3,STATE4, STATE5,STATE6,STATE7],L1),
    res0(shoot, mietus, STATE2, L1),
    res0_min(shoot, mietus, STATE2, [STATE2]),
    sort([STATE0,STATE1,STATE2,STATE3,STATE4, STATE5,STATE6,STATE7],L2),
    res0_plus(shoot, mietus, STATE2, L2),
    resN(shoot, mietus, STATE2, [STATE2]),
    resAb(shoot, mietus, STATE2, []).

test(shoot_mietus_3) :-
    state(state3, STATE3),
    state(state5, STATE5),
    state(state4, STATE4),

    sort([STATE4, STATE5],L1),
    res0(shoot, mietus, STATE3, L1),
    res0_min(shoot, mietus, STATE3, [STATE5]),
    sort([STATE4,STATE5],L2),
    res0_plus(shoot, mietus, STATE3,L2),
    resN(shoot, mietus, STATE3, [STATE5]),
    resAb(shoot, mietus, STATE3, []).

test(shoot_hador_6) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state5, STATE5),
    state(state4, STATE4),
    state(state6, STATE6),

    sort([STATE0, STATE1, STATE4, STATE5],L1),
    res0(shoot, hador, STATE6, L1),
    res0_min(shoot, hador, STATE6, [STATE4]),
    sort([STATE4,STATE5],L2),
    res0_plus(shoot, hador, STATE6, L2),
    resN(shoot, hador, STATE6, [STATE4]),
    resAb(shoot, hador, STATE6, []).

test(shoot_mietus_0) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state5, STATE5),
    state(state7, STATE7),
    state(state2, STATE2),
    state(state4, STATE4),
    state(state6, STATE6),

    sort([STATE0,STATE1,STATE2,STATE3,STATE4, STATE5,STATE6,STATE7],L1),
    res0(shoot, mietus, STATE0, L1),
    res0_min(shoot, mietus, STATE0, [STATE0]),
    res0_plus(shoot, mietus, STATE0, L1),
    resN(shoot, mietus, STATE0, [STATE0]),
    resAb(shoot, mietus, STATE0, []).

test(shoot_mietus_1) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state5, STATE5),
    state(state4, STATE4),

    sort([STATE0,STATE1,STATE4,STATE5],L1),
    res0(shoot, mietus, STATE1, L1),
    res0_min(shoot, mietus, STATE1, [STATE1]),
    res0_plus(shoot, mietus, STATE1, L1),
    resN(shoot, mietus, STATE1, [STATE1]),
    resAb(shoot, mietus, STATE1, []).

test(shoot_hador_7) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state5, STATE5),
    state(state7, STATE7),
    state(state2, STATE2),
    state(state4, STATE4),
    state(state6, STATE6),

    sort([STATE0, STATE1, STATE2,STATE3,STATE4, STATE5,STATE6,STATE7],L1),
    res0(shoot, hador, STATE7, L1),
    res0_min(shoot, hador, STATE7, [STATE7]),
    res0_plus(shoot, hador, STATE7, L1),
    resN(shoot, hador, STATE7, [STATE7]),
    resAb(shoot, hador, STATE7, []).

test(shoot_hador_4) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state5, STATE5),
    state(state4, STATE4),

    sort([STATE0,STATE1,STATE4,STATE5],L1),
    res0(shoot, hador, STATE4,L1),
    res0_min(shoot, hador, STATE4, [STATE4]),
    res0_plus(shoot, hador, STATE4, L1),
    resN(shoot, hador, STATE4, [STATE4]),
    resAb(shoot, hador, STATE4, []).


:- end_tests(resy).
