:- [states].

:- begin_tests(resy).

test(chown_hador_0) :-
    res0(chown, hador, state0, [state1, state3, state5, state7]),
    res0_min(chown, hador, state0, [state1,state2]),
    res0_plus(chown, hador, state0, [state1, state3, state5, state7]),
    resN(chown, hador, state0, [state1]),
    resAb(chown, hador, state0, []).

:- end_tests(resy).
