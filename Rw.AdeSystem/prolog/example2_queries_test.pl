:- [engine], [example2].

:- begin_tests(example2_queries).

test(possibly_1) :-
    possibly([alive],[entice,shoot],[epsilon,hador],[not_alive,has_gun_hador]).
test(always_1) :-
    not(always([alive],[entice,shoot],[epsilon,hador],[not_alive,has_gun_hador])).
test(typically_1) :-
    not(typically([alive],[entice,shoot],[epsilon,hador],[not_alive,has_gun_hador])).

test(possibly_2) :-
    possibly([alive],[entice,shoot],[epsilon,hador],[not_alive,not_has_gun_hador]).
test(always_2) :-
    not(always([alive],[entice,shoot],[epsilon,hador],[not_alive,not_has_gun_hador])).
test(typically_2) :-
    typically([alive],[entice,shoot],[epsilon,hador],[not_alive,not_has_gun_hador]).

test(possibly_3) :-
    possibly([alive],[entice,shoot],[epsilon,hador],[not_alive]).
test(always_3) :-
    not(always([alive],[entice,shoot],[epsilon,hador],[not_alive])).
test(typically_3) :-
    not(typically([alive],[entice,shoot],[epsilon,hador],[not_alive])).


:- end_tests(example2_queries).
