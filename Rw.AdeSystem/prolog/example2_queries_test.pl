:- [engine], [example2].

:- begin_tests(example2_queries).

test(possibly_1) :-
    possibly_after([[alive]],[entice,shoot],[epsilon,hador],[[not_alive,has_gun_hador]]).
test(always_1) :-
    not(always_after([[alive]],[entice,shoot],[epsilon,hador],[[not_alive,has_gun_hador]])).
test(typically_1) :-
    not(typically_after([[alive]],[entice,shoot],[epsilon,hador],[[not_alive,has_gun_hador]])).

test(possibly_2) :-
    possibly_after([[alive]],[entice,shoot],[epsilon,hador],[[not_alive,not_has_gun_hador]]).
test(always_2) :-
    not(always_after([[alive]],[entice,shoot],[epsilon,hador],[[not_alive,not_has_gun_hador]])).
test(typically_2) :-
    typically_after([[alive]],[entice,shoot],[epsilon,hador],[[not_alive,not_has_gun_hador]]).

test(possibly_3) :-
    possibly_after([[alive]],[entice,shoot],[epsilon,hador],[[not_alive]]).
test(always_3) :-
    not(always_after([[alive]],[entice,shoot],[epsilon,hador],[[not_alive]])).
test(typically_3) :-
    not(typically_after([[alive]],[entice,shoot],[epsilon,hador],[[not_alive]])).


test(possibly_involved_3x) :-
    possibly_involved(mietus,[shoot,entice], [epsilon,epsilon]).
test(always_involved_3x) :-
    not(always_involved(mietus,[shoot,entice], [epsilon,epsilon])).
test(typically_involved_3x) :-
    typically_involved(mietus,[shoot,entice], [epsilon,epsilon]).

test(possibly_involved_4) :-
    possibly_involved(hador,[shoot,entice], [epsilon,epsilon]).
test(always_involved_4) :-
    not(always_involved(hador,[shoot,entice], [epsilon,epsilon])).
test(typically_involved_4) :-
    not(typically_involved(hador,[shoot,entice], [epsilon,epsilon])).

test(possibly_involved_5) :-
    possibly_involved(mietus,[shoot,shoot, entice], [hador,mietus,mietus]).
test(always_involved_5) :-
    always_involved(mietus,[shoot,shoot,entice], [hador,mietus,mietus]).
test(typically_involved_5) :-
    typically_involved(mietus,[shoot,shoot,entice], [hador,mietus,mietus]).

test(possibly_involved_6) :-
    possibly_involved(hador,[shoot,shoot, entice], [hador,mietus,mietus]).
test(always_involved_6) :-
    always_involved(hador,[shoot,shoot,entice], [hador,mietus,mietus]).
test(typically_involved_6) :-
    typically_involved(hador,[shoot,shoot,entice], [hador,mietus,mietus]).

test(possibly_involved_7) :-
    possibly_involved(mietus,[shoot,shoot, entice], [hador,mietus,hador]).
test(always_involved_7) :-
    not(always_involved(mietus,[shoot,shoot,entice], [hador,mietus,hador])).
test(typically_involved_7) :-
    typically_involved(mietus,[shoot,shoot,entice], [hador,mietus,hador]).

test(possibly_involved_8) :-
    possibly_involved(hador,[shoot,shoot, entice], [hador,mietus,hador]).
test(always_involved_8) :-
    not(always_involved(hador,[shoot,shoot,entice], [hador,mietus,hador])).
test(typically_involved_8) :-
    typically_involved(hador,[shoot,shoot,entice], [hador,mietus,hador]).

test(possibly_involved_9) :-
    possibly_involved(mietus,[shoot,shoot,entice], [hador,mietus,hador]).
test(always_involved_9) :-
    not(always_involved(mietus,[shoot,shoot,entice], [hador,mietus,hador])).
test(typically_involved_9) :-
    typically_involved(mietus,[shoot,shoot,entice], [hador,mietus,hador]).

test(possibly_involved_10) :-
    possibly_involved(hador,[shoot,shoot, entice], [hador,mietus,epsilon]).
test(always_involved_10) :-
    not(always_involved(hador,[shoot,shoot,entice], [hador,mietus,epsilon])).
test(typically_involved_10) :-
    typically_involved(hador,[shoot,shoot,entice], [hador,mietus,epsilon]).


test(executable_1) :-
 	possibly_executable([[not_alive, not_walking]], [entice], [mietus]).
test(executable_2) :-
 	not( possibly_executable([[not_alive, not_walking]], [entice], [hador])).
test(executable_3) :-
	possibly_executable([[not_walking]], [entice,entice], [mietus,hador]).
test(executable_4) :-
	not( always_executable([[not_walking]], [entice,entice], [mietus,hador])).
test(executable_5) :-
 	always_executable([[not_alive, not_walking]], [shoot], [mietus]).
test(executable_6) :-
 	 always_executable([[not_alive, not_walking]], [entice], [mietus]).
test(executable_7) :-
	not( always_executable([[not_walking]], [entice,entice], [mietus,hador])).
test(executable_8) :-
	not( always_executable([[not_walking]], [shoot,entice], [mietus,hador])).

:- end_tests(example2_queries).
