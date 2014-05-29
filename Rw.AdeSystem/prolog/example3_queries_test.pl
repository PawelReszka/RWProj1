:- [engine], [example2].

:- begin_tests(example2_queries).

test(possibly_1) :-
    possibly([switch2_off,switch1_off],[turn1,turn2],[epsilon,epsilon], [switch1_on, switch2_on]).
test(always_1) :-
    always([switch2_off,switch1_off],[turn1,turn2],[epsilon,epsilon], [switch1_on, switch2_on]).
test(typically_1) :-
    typically([switch2_off,switch1_off],[turn1,turn2],[epsilon,epsilon], [switch1_on, switch2_on]).

test(possibly_2) :-
    possibly([switch2_off,switch1_off],[turn2,turn1],[epsilon,epsilon], [switch1_on, switch2_on]).
test(always_2) :-
    not(always([switch2_off,switch1_off],[turn2,turn1],[epsilon,epsilon], [switch1_on, switch2_on])).
test(typically_2) :-
    not(typically([switch2_off,switch1_off],[turn2,turn1],[epsilon,epsilon], [switch1_on, switch2_on])).


:- end_tests(example2_queries).
