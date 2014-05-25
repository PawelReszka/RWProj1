fluent(has_gun_hador).
fluent(has_gun_mietus).
fluent(alive).
fluent(walking).

action(shoot).
action(chown).
action(entice).

preserve(_,_,[]).

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

initially([has_gun_hador,not_has_gun_mietus,alive,walking]).

stmt(s1, [not_has_gun_hador, has_gun_mietus]).
stmt(s2, [not_has_gun_mietus,has_gun_hador]).

stmt(s3, [not_walking]).
stmt(s4, [alive]).

stmt(s5, [has_gun_mietus]).
stmt(s6, [has_gun_hador]).

formula(f, [s1, s2]).
formula(f2,[s3,s4]).

formula(f3, [s5]).
formula(f4, [s6]).

always(f).
always(f2).


state(state0, [has_gun_hador, not_has_gun_mietus, alive, walking]).
state(state1, [not_has_gun_hador, has_gun_mietus, alive, walking]).
state(state2, [has_gun_hador, not_has_gun_mietus, alive, not_walking]).
state(state3, [not_has_gun_hador, has_gun_mietus, alive, not_walking]).
state(state4, [has_gun_hador, not_has_gun_mietus, not_alive, not_walking]).
state(state5, [not_has_gun_hador, has_gun_mietus, not_alive, not_walking]).

causes(chown, mietus, [has_gun_hador, not_has_gun_mietus],[]).
causes(chown, hador, [not_has_gun_hador, has_gun_mietus], []).
causes(shoot, mietus, [not_alive], [has_gun_mietus]).

typically_causes(shoot, hador, [not_alive], [has_gun_hador]).

releases(shoot,mietus,[walking], f3).
releases(shoot,hador,[walking], f4).