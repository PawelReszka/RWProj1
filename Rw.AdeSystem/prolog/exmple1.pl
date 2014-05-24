fluent(has_gun_mietus).
fluent(has_gun_hador).
fluent(loaded).
fluent(alive).

action(load).
action(shoot).
action(chown).

executor(hador).
executor(mietus).

sneg(has_gun_mietus, not_has_gun_mietus).
sneg(has_gun_hador, not_has_gun_hador).
sneg(loaded, not_loaded).
sneg(alive, not_alive).

neg(X,Y) :- sneg(Y,X).
neg(X,Y) :- sneg(X,Y).

neg(X,X) :- !,fail.

sinertial(has_gun_mietus).
sinertial(has_gun_hador).
sinertial(loaded).
sinertial(chown).

inertial(X) :- sinertial(X).
inertial(X) :- neg(X,Y),
               sinertial(Y).

noninertial(X) :- not(inertial(X)).

initially([hador_ma_bron, not_has_gun_mietus, alive]).

state(state0, [has_gun_hador, not_has_gun_mietus, not_loaded, alive]).
state(state1, [not_has_gun_hador, has_gun_mietus, not_loaded, alive]).
state(state2, [has_gun_hador, not_has_gun_mietus, loaded, alive]). 
state(state3, [not_has_gun_hador, has_gun_mietus, loaded, alive]).
state(state4, [has_gun_hador, not_has_gun_mietus, not_loaded, not_alive]).
state(state5, [not_has_gun_hador, has_gun_mietus, not_loaded, not_alive]).
state(state6, [has_gun_hador, not_has_gun_mietus, loaded, not_alive]).
state(state7, [not_has_gun_hador, has_gun_mietus, loaded, not_alive]).

causes(chown, hador, [has_gun_mietus, not_has_gun_hador], []).
causes(chown, mietus, [has_gun_hador, not_has_gun_mietus], []).
causes(load, epsilon, [loaded], []).

causes(shoot, mietus, [not_loaded], [has_gun_mietus]).
causes(shoot, hador, [not_loaded], [has_gun_hador]).

causes(shoot, mietus, [not_alive], [loaded, has_gun_mietus]).
typically_causes(shoot, hador, [not_alive], [loaded, has_gun_hador]).
