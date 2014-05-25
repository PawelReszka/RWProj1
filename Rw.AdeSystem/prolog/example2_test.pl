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
    

:- end_tests(resy_example2).
