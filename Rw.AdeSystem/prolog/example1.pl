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

sinertial(has_gun_mietus).
sinertial(has_gun_hador).
sinertial(loaded).
sinertial(alive).

initially([has_gun_hador, not_has_gun_mietus, alive,not_loaded]).

stmt(s1, [not_has_gun_hador, has_gun_mietus]).
stmt(s2, [not_has_gun_mietus,has_gun_hador]).

formula(f, [s1,s2]).

always(f).

state(state0, [has_gun_hador, not_has_gun_mietus, not_loaded, alive]).
state(state1, [not_has_gun_hador, has_gun_mietus, not_loaded, alive]).
state(state2, [has_gun_hador, not_has_gun_mietus, loaded, alive]). 
state(state3, [not_has_gun_hador, has_gun_mietus, loaded, alive]).
state(state4, [has_gun_hador, not_has_gun_mietus, not_loaded, not_alive]).
state(state5, [not_has_gun_hador, has_gun_mietus, not_loaded, not_alive]).
state(state6, [has_gun_hador, not_has_gun_mietus, loaded, not_alive]).
state(state7, [not_has_gun_hador, has_gun_mietus, loaded, not_alive]).

causes(chown, hador, [has_gun_mietus], []).
causes(chown, mietus, [has_gun_hador], []).
causes(load, epsilon, [loaded], []).

causes(shoot, mietus, [not_loaded], [has_gun_mietus]).
causes(shoot, hador, [not_loaded], [has_gun_hador]).

causes(shoot, mietus, [not_alive], [loaded, has_gun_mietus]).
typically_causes(shoot, hador, [not_alive], [loaded, has_gun_hador]).
