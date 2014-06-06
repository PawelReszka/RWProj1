:- [engine], [example1].

:- begin_tests(resy_accessible_1).

test(accessible_nalive_alive) :-
    not(possibly_accessible([alive],[not_alive])),
    possibly_accessible([not_alive],[alive]).

:- end_tests(resy_accessible_1).
