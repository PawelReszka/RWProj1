:- [engine],[example4].

:- begin_tests(resy_example4).

test(chown_mietus_0) :-
    state(state0, STATE0),
    state(state2, STATE2),
    state(state4, STATE4),

    sort([STATE0, STATE2,STATE4],L1),
    res0(chown, mietus, STATE0, L1),
    res0_min(chown,mietus, STATE0, [STATE0]),
    res0_plus(chown, mietus, STATE0, L1),
    resN(chown, mietus, STATE0, [STATE0]),
    resAb(chown, mietus, STATE0, []),
    resN_trunc(chown, mietus, STATE0, [STATE0]),
    resAb_trunc(chown, mietus, STATE0, []).

test(chown_hador_0) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state5, STATE5),

    sort([STATE1, STATE3,STATE5],L1),
    res0(chown, hador, STATE0, L1),
    res0_min(chown,hador, STATE0, [STATE1]),
    res0_plus(chown, hador, STATE0, L1),
    resN(chown, hador, STATE0, [STATE1]),
    resAb(chown, hador, STATE0, []),
    resN_trunc(chown, hador, STATE0, [STATE1]),
    resAb_trunc(chown, hador, STATE0, []).

test(shoot_mietus_0) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state5, STATE5),
    state(state2, STATE2),
    state(state4, STATE4),

    sort([STATE0, STATE1,STATE2,STATE3,STATE4,STATE5],L1),
    res0(shoot, mietus, STATE0, L1),
    res0_min(shoot,mietus, STATE0, [STATE0]),
    res0_plus(shoot, mietus, STATE0, L1),
    resN(shoot, mietus, STATE0, [STATE0]),
    resAb(shoot, mietus, STATE0, []),
    resN_trunc(shoot, mietus, STATE0, [STATE0]),
    resAb_trunc(shoot, mietus, STATE0, []).

test(shoot_hador_0) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state5, STATE5),
    state(state2, STATE2),
    state(state4, STATE4),

    sort([STATE0, STATE1,STATE2,STATE3,STATE4,STATE5],L1),
    sort([STATE0,STATE2],L2),
    sort([STATE4,STATE5],L3),
    res0(shoot, hador, STATE0, L1),
    res0_min(shoot,hador, STATE0, L2),
    res0_plus(shoot, hador, STATE0, L3),
    resN(shoot, hador, STATE0, [STATE4]),
    resAb(shoot, hador, STATE0, L2),
    resN_trunc(shoot, hador, STATE0, [STATE4]),
    resAb_trunc(shoot, hador, STATE0, L2).

test(shoot_mietus_1) :-
    state(state1, STATE1),
    state(state5, STATE5),
    state(state4, STATE4),

    res0(shoot, mietus, STATE1, [STATE4,STATE5]),
    res0_min(shoot,mietus, STATE1, [STATE5]),
    res0_plus(shoot, mietus, STATE1, [STATE4,STATE5 ]),
    resN(shoot, mietus, STATE1, [STATE5]),
    resAb(shoot, mietus, STATE1, []),
    resN_trunc(shoot, mietus, STATE1, [STATE5]),
    resAb_trunc(shoot, mietus, STATE1, []).

test(shoot_hador_1) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state5, STATE5),
    state(state2, STATE2),
    state(state4, STATE4),

    sort([STATE0, STATE1,STATE2,STATE3,STATE4,STATE5],L1),
    res0(shoot, hador, STATE1, L1),
    res0_min(shoot,hador, STATE1, [STATE1]),
    res0_plus(shoot, hador, STATE1, L1),
    resN(shoot, hador, STATE1, [STATE1]),
    resAb(shoot, hador, STATE1, []),
    resN_trunc(shoot, hador, STATE1, [STATE1]),
    resAb_trunc(shoot, hador, STATE1, []).

test(shoot_mietus_2) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state5, STATE5),
    state(state2, STATE2),
    state(state4, STATE4),

    sort([STATE0, STATE1,STATE2,STATE3,STATE4,STATE5],L1),
    res0(shoot, mietus, STATE2, L1),
    res0_min(shoot,mietus, STATE2, [STATE2]),
    res0_plus(shoot, mietus, STATE2, L1),
    resN(shoot, mietus, STATE2, [STATE2]),
    resAb(shoot, mietus, STATE2, []),
    resN_trunc(shoot, mietus, STATE2, [STATE2]),
    resAb_trunc(shoot, mietus, STATE2, []).

test(shoot_hador_2) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state5, STATE5),
    state(state2, STATE2),
    state(state4, STATE4),

    sort([STATE0, STATE1,STATE2,STATE3,STATE4,STATE5],L1),
    res0(shoot, hador, STATE2, L1),
    sort([STATE0,STATE2],L2),
    res0_min(shoot,hador, STATE2, L2),
    sort([STATE4,STATE5],L3),
    res0_plus(shoot, hador, STATE2, L3),
    resN(shoot, hador, STATE2, [STATE4]),
    resAb(shoot, hador, STATE2, L2),
    resN_trunc(shoot, hador, STATE2, [STATE4]),
    resAb_trunc(shoot, hador, STATE2, L2).

test(shoot_mietus_3) :-
    state(state3, STATE3),
    state(state5, STATE5),
    state(state4, STATE4),

    res0(shoot, mietus, STATE3, [STATE4,STATE5]),
    res0_min(shoot,mietus, STATE3, [STATE5]),
    res0_plus(shoot, mietus, STATE3, [STATE4,STATE5]),
    resN(shoot, mietus, STATE3, [STATE5]),
    resAb(shoot, mietus, STATE3, []),
    resN_trunc(shoot, mietus, STATE3, [STATE5]),
    resAb_trunc(shoot, mietus, STATE3, []).

test(shoot_hador_3) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state5, STATE5),
    state(state2, STATE2),
    state(state4, STATE4),

    sort([STATE0, STATE1,STATE2,STATE3,STATE4,STATE5],L1),
    res0(shoot, hador, STATE3, L1),
    res0_min(shoot,hador, STATE3, [STATE3]),
    res0_plus(shoot, hador, STATE3, L1),
    resN(shoot, hador, STATE3, [STATE3]),
    resAb(shoot, hador, STATE3, []),
    resN_trunc(shoot, hador, STATE3, [STATE3]),
    resAb_trunc(shoot, hador, STATE3, []).

test(shoot_mietus_4) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state5, STATE5),
    state(state2, STATE2),
    state(state4, STATE4),


    sort([STATE0, STATE1,STATE2,STATE3,STATE4,STATE5],L1),
    res0(shoot, mietus, STATE4, L1),
    res0_min(shoot,mietus, STATE4, [STATE4]),
   res0_plus(shoot, mietus, STATE4, L1),
    resN(shoot, mietus, STATE4, [STATE4]),
    resAb(shoot, mietus, STATE4, []),
    resN_trunc(shoot, mietus, STATE4, [STATE4]),
    resAb_trunc(shoot, mietus, STATE4, []).

test(shoot_hador_4) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state5, STATE5),
    state(state2, STATE2),
    state(state4, STATE4),


    sort([STATE0,STATE1,STATE2,STATE3,STATE4,STATE5],L1),
    res0(shoot, hador, STATE4, L1),
    sort([STATE0,STATE4],L2),
    res0_min(shoot,hador, STATE4, L2), % STATE4 ma na pewno 0 różnicy, a takich jest tylko 1
    sort([STATE4,STATE5],L3),
    res0_plus(shoot, hador, STATE4,L3),
    resN(shoot, hador, STATE4, [STATE4]),
    resAb(shoot, hador, STATE4, [STATE0]),
    resN_trunc(shoot, hador, STATE4, [STATE4]),
    resAb_trunc(shoot, hador, STATE4, [STATE0]).

test(shoot_mietus_5) :-
    state(state5, STATE5),
    state(state4, STATE4),

    res0(shoot, mietus, STATE5, [STATE4,STATE5]),
    res0_min(shoot,mietus, STATE5, [STATE5]),
   res0_plus(shoot, mietus, STATE5, [STATE4,STATE5]),
    resN(shoot, mietus, STATE5, [STATE5]),
    resAb(shoot, mietus, STATE5, []),
    resN_trunc(shoot, mietus, STATE5, [STATE5]),
    resAb_trunc(shoot, mietus, STATE5, []).

test(shoot_hador_5) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),
    state(state5, STATE5),
    state(state2, STATE2),
    state(state4, STATE4),

    sort([STATE0,STATE1,STATE2,STATE3,STATE4,STATE5],L1),

    res0(shoot, hador, STATE5, L1),
    res0_min(shoot,hador, STATE5, [STATE5]),
    res0_plus(shoot, hador, STATE5, L1),
    resN(shoot, hador, STATE5, [STATE5]),
    resAb(shoot, hador, STATE5, []),
    resN_trunc(shoot, hador, STATE5, [STATE5]),
    resAb_trunc(shoot, hador, STATE5, []).


:- end_tests(resy_example4).
