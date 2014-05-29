:- [engine], [example2].

:- begin_tests(example2_queries).

test(test1) :-
    possibly([alive],[entice,shoot],[epsilon,hador],[not_alive,has_gun_hador]),
    not(always([alive],[entice,shoot],[epsilon,hador],[not_alive,has_gun_hador])),
    not(typically([alive],[entice,shoot],[epsilon,hador],[not_alive,has_gun_hador])).



:- end_tests(example2_queries).
