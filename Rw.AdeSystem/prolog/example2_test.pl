:- [engine],[example2].

:- begin_tests(resy_example2).

test(entice_hador_0) :-
    state(state0, S0),
    state(state1, S1),
    sort(S0, STATE0),
    sort(S1, STATE1),

    res0(entice, hador, STATE0, [STATE0, STATE1]),
    res0_min(entice,hador, STATE0, [STATE0]),
    res0_plus(entice, hador, STATE0, [STATE0, STATE1]),
    resN(entice, hador, STATE0, [STATE0]),
    resAb(entice, hador, STATE0, []),
    resN_trunc(entice, hador, STATE0, [STATE0]),
    resAb_trunc(entice, hador, STATE0, []).

test(entice_hador_1) :-
    state(state0, S0),
    state(state1, S1),
    sort(S0, STATE0),
    sort(S1, STATE1),

    res0(entice, hador, STATE1, [STATE0, STATE1]),
    res0_min(entice,hador, STATE1, [STATE1]),
    res0_plus(entice, hador, STATE1, [STATE0, STATE1]),
    resN(entice, hador, STATE1, [STATE1]),
    resAb(entice, hador, STATE1, []),
    resN_trunc(entice, hador, STATE1, [STATE1]),
    resAb_trunc(entice, hador, STATE1, []).

test(entice_hador_2) :-
    state(state0, S0),
    state(state1, S1),
    state(state2, S2),
    sort(S0,STATE0),
    sort(S1,STATE1),
    sort(S2,STATE2),

    res0(entice, hador, STATE2, [STATE0, STATE1]),
    res0_min(entice,hador, STATE2, [STATE0]),
    res0_plus(entice, hador, STATE2, [STATE0, STATE1]),
    resN(entice, hador, STATE2, [STATE0]),
    resAb(entice, hador, STATE2, []),
    resN_trunc(entice, hador, STATE2, [STATE0]),
    resAb_trunc(entice, hador, STATE2, []).

test(entice_hador_3) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state3, STATE3),

    res0(entice, hador, STATE3, [STATE0, STATE1]),
    res0_min(entice,hador, STATE3, [STATE1]),
    res0_plus(entice, hador, STATE3, [STATE0, STATE1]),
    resN(entice, hador, STATE3, [STATE1]),
    resAb(entice, hador, STATE3, []),
    resN_trunc(entice, hador, STATE3, [STATE1]),
    resAb_trunc(entice, hador, STATE3, []).

test(entice_hador_4) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state4, STATE4),

    res0(entice, hador, STATE4, [STATE0, STATE1]),
    res0_min(entice,hador, STATE4, [STATE0]),
    res0_plus(entice, hador, STATE4, [STATE0, STATE1]),
    resN(entice, hador, STATE4, [STATE0]),
    resAb(entice, hador, STATE4, []),
    resN_trunc(entice, hador, STATE4, []),
    resAb_trunc(entice, hador, STATE4, []).

test(entice_hador_5) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state5, STATE5),

    res0(entice, hador, STATE5, [STATE0, STATE1]),
    res0_min(entice,hador, STATE5, [STATE1]),
    res0_plus(entice, hador, STATE5, [STATE0, STATE1]),
    resN(entice, hador, STATE5, [STATE1]),
    resAb(entice, hador, STATE5, []),
    resN_trunc(entice, hador, STATE5, []),
    resAb_trunc(entice, hador, STATE5, []).

test(entice_mietus_0) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state2, STATE2),
    state(state3, STATE3),
    state(state4, STATE4),
    state(state5, STATE5),

    sort([STATE0, STATE1,STATE2,STATE3,STATE4,STATE5],L1),
    sort([STATE0, STATE1],L2),
    res0(entice, mietus, STATE0, L1),
    res0_min(entice,mietus, STATE0, [STATE0]),
    res0_plus(entice, mietus, STATE0, L2),
    resN(entice, mietus, STATE0, [STATE0]),
    resAb(entice, mietus, STATE0, []),
    resN_trunc(entice, mietus, STATE0, [STATE0]),
    resAb_trunc(entice, mietus, STATE0, []).

test(entice_mietus_1) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state2, STATE2),
    state(state3, STATE3),
    state(state4, STATE4),
    state(state5, STATE5),

    sort([STATE0, STATE1,STATE2,STATE3,STATE4,STATE5],L1),
    sort([STATE0, STATE1],L2),
    res0(entice, mietus, STATE1, L1),
    res0_min(entice,mietus, STATE1, [STATE1]),
    res0_plus(entice, mietus, STATE1, L2),
    resN(entice, mietus, STATE1, [STATE1]),
    resAb(entice, mietus, STATE1, []),
    resN_trunc(entice, mietus, STATE1, [STATE1]),
    resAb_trunc(entice, mietus, STATE1, []).

test(entice_mietus_2) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state2, STATE2),
    state(state3, STATE3),
    state(state4, STATE4),
    state(state5, STATE5),

    sort([STATE0, STATE1,STATE2,STATE3,STATE4,STATE5],L1),
    sort([STATE0, STATE1],L2),

    res0(entice, mietus, STATE2, L1),
    res0_min(entice,mietus, STATE2, [STATE2]),
    res0_plus(entice, mietus, STATE2, L2),
    resN(entice, mietus, STATE2, [STATE0]),
    resAb(entice, mietus, STATE2, [STATE2]),
    resN_trunc(entice, mietus, STATE2, [STATE0]),
    resAb_trunc(entice, mietus, STATE2, [STATE2]).

test(entice_mietus_3) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state2, STATE2),
    state(state3, STATE3),
    state(state4, STATE4),
    state(state5, STATE5),

    sort([STATE0, STATE1,STATE2,STATE3,STATE4,STATE5],L1),
    sort([STATE0, STATE1],L2),
    res0(entice, mietus, STATE3, L1),
    res0_min(entice,mietus, STATE3, [STATE3]),
    res0_plus(entice, mietus, STATE3, L2),
    resN(entice, mietus, STATE3, [STATE1]),
    resAb(entice, mietus, STATE3, [STATE3]),
    resN_trunc(entice, mietus, STATE3, [STATE1]),
    resAb_trunc(entice, mietus, STATE3, [STATE3]).

test(entice_mietus_4) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state2, STATE2),
    state(state3, STATE3),
    state(state4, STATE4),
    state(state5, STATE5),

    sort([STATE0, STATE1,STATE2,STATE3,STATE4,STATE5],L1),
    sort([STATE0, STATE1],L2),

    res0(entice, mietus, STATE4, L1),
    res0_min(entice,mietus, STATE4, [STATE4]),
    res0_plus(entice, mietus, STATE4, L2),
    resN(entice, mietus, STATE4, [STATE0]),
    resAb(entice, mietus, STATE4, [STATE4]),
    resN_trunc(entice, mietus, STATE4, [STATE0]),
    resAb_trunc(entice, mietus, STATE4, [STATE4]).

test(entice_mietus_5) :-
    state(state0, STATE0),
    state(state1, STATE1),
    state(state2, STATE2),
    state(state3, STATE3),
    state(state4, STATE4),
    state(state5, STATE5),

    sort([STATE0, STATE1,STATE2,STATE3,STATE4,STATE5],L1),
    sort([STATE0, STATE1],L2),
    res0(entice, mietus, STATE5, L1),
    res0_min(entice,mietus, STATE5, [STATE5]),
    res0_plus(entice, mietus, STATE5, L2),
    resN(entice, mietus, STATE5, [STATE1]),
    resAb(entice, mietus, STATE5, [STATE5]),
    resN_trunc(entice, mietus, STATE5, [STATE1]),
    resAb_trunc(entice, mietus, STATE5, [STATE5]).

test(shoot_hador_0) :-
    state(state0, STATE0),
    state(state4, STATE4),

    resN(shoot,hador,STATE0,[STATE4]),
    resAb(shoot,hador,STATE0,[STATE0]).

test(shoot_hador_1) :-
    state(state1, STATE1),

    resN(shoot,hador,STATE1,[STATE1]),
    resAb(shoot,hador,STATE1,[]).

test(shoot_hador_2) :-
    state(state2, STATE2),
    state(state4, STATE4),

    resN(shoot,hador,STATE2,[STATE4]),
    resAb(shoot,hador,STATE2,[STATE2]).

test(shoot_hador_3) :-
    state(state3, STATE3),
   
    resN(shoot,hador,STATE3,[STATE3]),
    resAb(shoot,hador,STATE3,[]).

test(shoot_hador_4) :-
    state(state4, STATE4),

    resN(shoot,hador,STATE4,[STATE4]),
    resAb(shoot,hador,STATE4,[]).

test(shoot_hador_5) :-
    state(state5, STATE5),

    resN(shoot,hador,STATE5,[STATE5]),
    resAb(shoot,hador,STATE5,[]).

test(shoot_mietus_0) :-
    state(state0, STATE0),
    resN(shoot,mietus,STATE0,[STATE0]),
    resAb(shoot,mietus,STATE0,[]).

test(shoot_mietus_1) :-
    state(state1, STATE1),
    state(state5, STATE5),

    resN(shoot,mietus,STATE1,[STATE5]),
    resAb(shoot,mietus,STATE1,[]).

test(shoot_mietus_2) :-
    state(state2, STATE2),
    resN(shoot,mietus,STATE2,[STATE2]),
    resAb(shoot,mietus,STATE2,[]).

test(shoot_mietus_3) :-
        state(state3, STATE3),
    state(state5, STATE5),

    resN(shoot,mietus,STATE3,[STATE5]),
    resAb(shoot,mietus,STATE3,[]).

test(shoot_mietus_4) :-
    state(state4, STATE4),
    resN(shoot,mietus,STATE4,[STATE4]),
    resAb(shoot,mietus,STATE4,[]).

test(shoot_mietus_5) :-
    state(state5, STATE5),

    resN(shoot,mietus,STATE5,[STATE5]),
    resAb(shoot,mietus,STATE5,[]).

:- end_tests(resy_example2).
