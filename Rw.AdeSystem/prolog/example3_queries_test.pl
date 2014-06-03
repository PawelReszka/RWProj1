:- [engine], [example3].

:- begin_tests(example3_queries).

test(possibly_1) :-
    possibly([switch2_off,switch1_off],[turn1,turn2],[epsilon,epsilon], [switch1_on, switch2_on]).
test(always_1) :-
    not(always([switch2_off,switch1_off],[turn1,turn2],[epsilon,epsilon], [switch1_on, switch2_on])).
test(typically_1) :-
    not(typically([switch2_off,switch1_off],[turn1,turn2],[epsilon,epsilon], [switch1_on, switch2_on])).

test(possibly_2) :-
    possibly([switch2_off,switch1_off],[turn2,turn1],[epsilon,epsilon], [switch1_on, switch2_on]).
test(always_2) :-
    not(always([switch2_off,switch1_off],[turn2,turn1],[epsilon,epsilon], [switch1_on, switch2_on])).
test(typically_2) :-
    not(typically([switch2_off,switch1_off],[turn2,turn1],[epsilon,epsilon], [switch1_on, switch2_on])).

test(possibly_3) :-
    possibly_involved(hador, [turn2,turn1], [epsilon,epsilon]).
test(always_3) :-
    not(always_involved(hador, [turn2,turn1], [epsilon,epsilon])).
test(typically_3) :-
    not(typically_involved(hador, [turn2,turn1], [epsilon,epsilon])).

test(possibly_4) :-
    possibly_involved(mietus, [turn2,turn1], [epsilon,epsilon]).
test(always_4) :-
    not(always_involved(mietus, [turn2,turn1], [epsilon,epsilon])).
test(typically_4) :-
    not(typically_involved(mietus, [turn2,turn1], [epsilon,epsilon])).


:- end_tests(example3_queries).
