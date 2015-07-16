( mutual recursion )

defer odd
: even ( n -- f )
	case
	0 of true  endof
	1 of false endof
	1- odd dup
	endcase
;

:noname ( n -- f )
	case
	1 of true  endof
	0 of false endof
	1- even dup
	endcase
; is odd


0 [if]
: >>> dup . ;
: test ( n m -- )
	?do
		i >>> odd  ." odd  = " . cr
		i >>> even ." even = " . cr
	loop
;

[else]

: (test) ( xt n -- )
	dup . over execute
	if ." -> " else ." >< " then >name .name ;

: .tabstop 9 emit ;

: test ( n m -- )
	['] even -rot ['] odd  -rot
	?do 2dup
		i (test) .tabstop i (test) cr
	loop 2drop
;
[then]

3759 3750 test

bye
