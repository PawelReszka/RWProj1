fluent(has_gun_hador).
fluent(has_gun_mietus).
fluent(alive).
fluent(walking).

action(shoot).
action(chown).
action(entice).

executor(hador).
executor(mietus).

sneg(has_gun_hador, not_has_gun_hador).
sneg(has_gun_mietus, not_has_gun_mietus).
sneg(alive, not_alive).
sneg(walking, not_walking).

sinertial(has_gun_mietus).
sinertial(has_gun_hador).
sinertial(alive).
sinertial(walking).

initially_after([],[],[]).
initially_after([],[],[has_gun_hador,not_has_gun_mietus,alive,walking]).

always([[not_has_gun_hador, has_gun_mietus], [not_has_gun_mietus,has_gun_hador]]).
always([[not_walking],[alive]]).

state(state0, [has_gun_hador, not_has_gun_mietus, alive, walking]).
state(state1, [not_has_gun_hador, has_gun_mietus, alive, walking]).
state(state2, [has_gun_hador, not_has_gun_mietus, alive, not_walking]).
state(state3, [not_has_gun_hador, has_gun_mietus, alive, not_walking]).
state(state4, [has_gun_hador, not_has_gun_mietus, not_alive, not_walking]).
state(state5, [not_has_gun_hador, has_gun_mietus, not_alive, not_walking]).

causes(chown, mietus, [[has_gun_hador]],[[]]).
causes(chown, hador, [[has_gun_mietus]], [[]]).
causes(shoot, mietus, [[not_alive]], [[has_gun_mietus]]).
causes(entice, hador, [[walking]],[[]]).

typically_causes(entice, mietus, [[walking]],[[]]).
typically_causes(shoot, hador, [[not_alive]], [[has_gun_hador]]).

preserve(entice, hador, [alive]).
