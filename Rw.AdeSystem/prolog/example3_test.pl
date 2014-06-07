:- [engine],[example3].

:- begin_tests(resy_example3).

test(turn1_hador_0) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state2, STATE2),

    res0(turn1, hador, STATE0, [STATE1, STATE2]),
    res0_min(turn1, hador, STATE0, [STATE1]),
    res0_plus(turn1, hador, STATE0, [STATE1,STATE2]),
    resN(turn1,hador,STATE0,[STATE1]),
    resAb(turn1, hador, STATE0, []),
    resN_trunc(turn1,hador,STATE0,[STATE1]),
    resAb_trunc(turn1, hador, STATE0, []).

test(turn1_hador_1) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),

    res0(turn1, hador, STATE1, [STATE0, STATE3]),
    res0_min(turn1, hador, STATE1, [STATE0]),
    res0_plus(turn1, hador, STATE1, [STATE0,STATE3]),
    resN(turn1,hador,STATE1,[STATE0]),
    resAb(turn1, hador, STATE1, []),
    resN_trunc(turn1,hador,STATE1,[STATE0]),
    resAb_trunc(turn1, hador, STATE1, []).

test(turn1_hador_2) :-
    state(state0, STATE0),
    state(state3, STATE3),
    state(state2, STATE2),

    res0(turn1, hador, STATE2, [STATE0, STATE3]),
    res0_min(turn1, hador, STATE2, [STATE3]),
    res0_plus(turn1, hador, STATE2, [STATE0,STATE3]),
    resN(turn1,hador,STATE2,[STATE3]),
    resAb(turn1, hador, STATE2, []),
    resN_trunc(turn1,hador,STATE2,[STATE3]),
    resAb_trunc(turn1, hador, STATE2, []).

test(turn1_hador_3) :-
    state(state1, STATE1),
    state(state3, STATE3),
    state(state2, STATE2),

    res0(turn1, hador, STATE3, [STATE1, STATE2]),
    res0_min(turn1, hador, STATE3, [STATE2]),
    res0_plus(turn1, hador, STATE3, [STATE1,STATE2]),
    resN(turn1,hador,STATE3,[STATE2]),
    resAb(turn1, hador, STATE3, []),
    resN_trunc(turn1,hador,STATE3,[STATE2]),
    resAb_trunc(turn1, hador, STATE3, []).

test(turn1_mietus_0) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state2, STATE2),

    res0(turn1, mietus, STATE0, [STATE0,STATE1, STATE2,STATE3]),
    res0_min(turn1, mietus, STATE0, [STATE0]),
    res0_plus(turn1, mietus, STATE0, [STATE1,STATE2]),
    resN(turn1,mietus,STATE0,[STATE1]),
    resAb(turn1, mietus, STATE0, [STATE0]),
    resN_trunc(turn1,mietus,STATE0,[STATE1]),
    resAb_trunc(turn1, mietus, STATE0, [STATE0]).

test(turn1_mietus_1) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state2, STATE2),

    res0(turn1, mietus, STATE1, [STATE0, STATE1,STATE2,STATE3]),
    res0_min(turn1, mietus, STATE1, [STATE1]),
    res0_plus(turn1, mietus, STATE1, [STATE0,STATE3]),
    resN(turn1,mietus,STATE1,[STATE0]),
    resAb(turn1, mietus, STATE1, [STATE1]),
    resN_trunc(turn1,mietus,STATE1,[STATE0]),
    resAb_trunc(turn1, mietus, STATE1, [STATE1]).

test(turn1_mietus_2) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state2, STATE2),

    res0(turn1, mietus, STATE2, [STATE0, STATE1,STATE2,STATE3]),
    res0_min(turn1, mietus, STATE2, [STATE2]),
    res0_plus(turn1, mietus, STATE2, [STATE0,STATE3]),
    resN(turn1,mietus,STATE2,[STATE3]),
    resAb(turn1, mietus, STATE2, [STATE2]),
    resN_trunc(turn1,mietus,STATE2,[STATE3]),
    resAb_trunc(turn1, mietus, STATE2, [STATE2]).

test(turn1_mietus_3) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state2, STATE2),

    res0(turn1, mietus, STATE3, [STATE0,STATE1, STATE2,STATE3]),
    res0_min(turn1, mietus, STATE3, [STATE3]),
    res0_plus(turn1, mietus, STATE3, [STATE1,STATE2]),
    resN(turn1,mietus,STATE3,[STATE2]),
    resAb(turn1, mietus, STATE3, [STATE3]),
    resN_trunc(turn1,mietus,STATE3,[STATE2]),
    resAb_trunc(turn1, mietus, STATE3, [STATE3]).

test(turn2_hador_0) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state2, STATE2),

    res0(turn2, hador, STATE0, [STATE0,STATE1, STATE2,STATE3]),
    res0_min(turn2, hador, STATE0, [STATE0]),
    res0_plus(turn2, hador, STATE0, [STATE2,STATE3]),
    resN(turn2,hador,STATE0,[STATE3]),
    resAb(turn2, hador, STATE0, [STATE0]),
    resN_trunc(turn2,hador,STATE0,[STATE3]),
    resAb_trunc(turn2, hador, STATE0, [STATE0]).

test(turn2_hador_1) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state2, STATE2),

    res0(turn2, hador, STATE1, [STATE0, STATE1, STATE2, STATE3]),
    res0_min(turn2, hador, STATE1, [STATE1]),
    res0_plus(turn2, hador, STATE1, [STATE2,STATE3]),
    resN(turn2,hador,STATE1,[STATE2]),
    resAb(turn2, hador, STATE1, [STATE1]),
    resN_trunc(turn2,hador,STATE1,[STATE2]),
    resAb_trunc(turn2, hador, STATE1, [STATE1]).

test(turn2_hador_2) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state2, STATE2),

    res0(turn2, hador, STATE2, [STATE0, STATE1]),
    res0_min(turn2, hador, STATE2, [STATE1]),
    res0_plus(turn2, hador, STATE2, [STATE0,STATE1]),
    resN(turn2,hador,STATE2,[STATE1]),
    resAb(turn2, hador, STATE2, []),
    resN_trunc(turn2,hador,STATE2,[STATE1]),
    resAb_trunc(turn2, hador, STATE2, []).

test(turn2_hador_3) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),

    res0(turn2, hador, STATE3, [STATE0, STATE1]),
    res0_min(turn2, hador, STATE3, [STATE0]),
    res0_plus(turn2, hador, STATE3, [STATE0,STATE1]),
    resN(turn2,hador,STATE3,[STATE0]),
    resAb(turn2, hador, STATE3, []),
    resN_trunc(turn2,hador,STATE3,[STATE0]),
    resAb_trunc(turn2, hador, STATE3, []).

test(turn2_mietus_0) :-
    state(state0, STATE0),
    state(state3, STATE3),
    state(state2, STATE2),
    state(state4, STATE4),
    state(state6, STATE6),

    res0(turn2, mietus, STATE0, [STATE2,STATE3]),
    res0_min(turn2, mietus, STATE0, [STATE3]),
    res0_plus(turn2, mietus, STATE0, [STATE2,STATE3]),
    resN(turn2,mietus,STATE0,[STATE3]),
    resAb(turn2, mietus, STATE0, []),
    resN_trunc(turn2,mietus,STATE0,[STATE3]),
    resAb_trunc(turn2, mietus, STATE0, []).

test(turn2_mietus_1) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state5, STATE5),
    state(state7, STATE7),
    state(state2, STATE2),
    state(state4, STATE4),
    state(state6, STATE6),

    res0(turn2, mietus, STATE1, [STATE2,STATE3]),
    res0_min(turn2, mietus, STATE1, [STATE2]),
    res0_plus(turn2, mietus, STATE1, [STATE2,STATE3]),
    resN(turn2,mietus,STATE1,[STATE2]),
    resAb(turn2, mietus, STATE1, []),
    resN_trunc(turn2,mietus,STATE1,[STATE2]),
    resAb_trunc(turn2, mietus, STATE1, []).

test(turn2_mietus_2) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state5, STATE5),
    state(state7, STATE7),
    state(state2, STATE2),
    state(state4, STATE4),
    state(state6, STATE6),

    res0(turn2, mietus, STATE2, [STATE0, STATE1,STATE2,STATE3]),
    res0_min(turn2, mietus, STATE2, [STATE2]),
    res0_plus(turn2, mietus, STATE2, [STATE0,STATE1]),
    resN(turn2,mietus,STATE2,[STATE1]),
    resAb(turn2, mietus, STATE2, [STATE2]),
    resN_trunc(turn2,mietus,STATE2,[STATE1]),
    resAb_trunc(turn2, mietus, STATE2, [STATE2]).

test(turn2_mietus_3) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state5, STATE5),
    state(state7, STATE7),
    state(state2, STATE2),
    state(state4, STATE4),
    state(state6, STATE6),

    res0(turn2, mietus, STATE3, [STATE0,STATE1, STATE2,STATE3]),
    res0_min(turn2, mietus, STATE3, [STATE3]),
    res0_plus(turn2, mietus, STATE3, [STATE0,STATE1]),
    resN(turn2,mietus,STATE3,[STATE0]),
    resAb(turn2, mietus, STATE3, [STATE3]),
    resN_trunc(turn2,mietus,STATE3,[STATE0]),
    resAb_trunc(turn2, mietus, STATE3, [STATE3]).


:- end_tests(resy_example3).
