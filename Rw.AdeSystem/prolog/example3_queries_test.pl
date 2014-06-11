:- [engine], [example3].

:- begin_tests(example3_queries).

test(possibly_1) :-
    possibly_after([[switch2_off,switch1_off]],[turn1,turn2],[epsilon,epsilon], [[switch1_on, switch2_on]]).
test(always_1) :-
    not(always_after([[switch2_off,switch1_off]],[turn1,turn2],[epsilon,epsilon], [[switch1_on, switch2_on]])).
test(typically_1) :-
    typically_after([[switch2_off,switch1_off]],[turn1,turn2],[epsilon,epsilon], [[switch1_on, switch2_on]]).

test(possibly_2) :-
    possibly_after([[switch2_off,switch1_off]],[turn2,turn1],[epsilon,epsilon], [[switch1_on, switch2_on]]).
test(always_2) :-
    not(always_after([[switch2_off,switch1_off]],[turn2,turn1],[epsilon,epsilon], [[switch1_on, switch2_on]])).
test(typically_2) :-
    typically_after([[switch2_off,switch1_off]],[turn2,turn1],[epsilon,epsilon], [[switch1_on, switch2_on]]).
% powinno byc tu TRUE, poniewaz turn1 w stanie 3 przez hador prowadzi nie do 3 tylko 2 -N

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
