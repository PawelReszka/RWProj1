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


test(possibly_3) :-
    possibly_involved(mietus,[shoot,entice], [epsilon,epsilon]).
test(always_3) :-
    not(always_involved(mietus,[shoot,entice], [epsilon,epsilon])).
test(typically_3) :-
    typically_involved(mietus,[shoot,entice], [epsilon,epsilon]).

test(possibly_4) :-
    possibly_involved(hador,[shoot,entice], [epsilon,epsilon]).
test(always_4) :-
    not(always_involved(hador,[shoot,entice], [epsilon,epsilon])).
test(typically_4) :-
    not(typically_involved(hador,[shoot,entice], [epsilon,epsilon])).

test(possibly_5) :-
    possibly_involved(mietus,[shoot,shootentice], [hador,mietus,mietus]).
test(always_5) :-
    always_involved(mietus,[shoot,shoot,entice], [hador,mietus,mietus]).
test(typically_5) :-
    typically_involved(mietus,[shoot,shoot,entice], [hador,mietus,mietus]).

test(possibly_6) :-
    possibly_involved(hador,[shoot,shootentice], [hador,mietus,mietus]).
test(always_6) :-
    always_involved(hador,[shoot,shoot,entice], [hador,mietus,mietus]).
test(typically_6) :-
    typically_involved(hador,[shoot,shoot,entice], [hador,mietus,mietus]).

test(possibly_7) :-
    possibly_involved(mietus,[shoot,shootentice], [hador,mietus,hador]).
test(always_7) :-
    no(always_involved(mietus,[shoot,shoot,entice], [hador,mietus,hador])).
test(typically_7) :-
    typically_involved(mietus,[shoot,shoot,entice], [hador,mietus,hador]).

test(possibly_8) :-
    possibly_involved(hador,[shoot,shootentice], [hador,mietus,hador]).
test(always_8) :-
    not(always_involved(hador,[shoot,shoot,entice], [hador,mietus,hador])).
test(typically_8) :-
    typically_involved(hador,[shoot,shoot,entice], [hador,mietus,hador]).

test(possibly_9) :-
    possibly_involved(mietus,[shoot,shootentice], [hador,mietus,hador]).
test(always_9) :-
    not(always_involved(mietus,[shoot,shoot,entice], [hador,mietus,hador])).
test(typically_9) :-
    typically_involved(mietus,[shoot,shoot,entice], [hador,mietus,hador]).

test(possibly_10) :-
    possibly_involved(hador,[shoot,shootentice], [hador,mietus,epsilon]).
test(always_10) :-
    not(always_involved(hador,[shoot,shoot,entice], [hador,mietus,epsilon])).
test(typically_10) :-
    typically_involved(hador,[shoot,shoot,entice], [hador,mietus,epsilon]).



:- end_tests(example2_queries).
