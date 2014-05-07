\ 
\ Copyright (C) 2013 Meng-Cheng Cheng (letoh)
\ 
\ Description:
\     simple recursive descent parsing demo
\ 
\     Grammars:
\       exp    := term ( ['+' | '-'] term ) *
\       term   := factor ( ['*' | '/'] factor ) *
\       factor := num



0 constant TKN_ERR
1 constant TKN_NUM
2 constant TKN_VAR
3 constant TKN_ADD
4 constant TKN_SUB
5 constant TKN_MUL
6 constant TKN_DIV
7 constant TKN_LPR
8 constant TKN_RPR
9 constant TKN_END


variable tkn.ref
variable tkn.val


: str>num ( s -- n flag )
	>r 0 s>d r> count >number ( ud a flag )
	>r drop d>s r> 0=
;

: pick.number? ( s -- s flag )
	dup str>num ( n flag )
		if tkn.val ! TKN_NUM tkn.ref ! true
		else drop false then
;

: var>value ( xt -- n )
	cell+ cell+ @
;

: pick.var? ( s -- s flag )
	dup find if var>value
		tkn.val ! TKN_VAR tkn.ref ! true
		else drop false then
;

: pick.oper: ( xt id a n -- )
	create
		( n  ) , ( a  ) , ( id ) , ( xt ) ,
	does> ( a d -- a flag )
	 	2dup 2@ rot count compare ( a d f )
		0= if cell+ cell+ 2@ ( a xt id )
			tkn.ref ! tkn.val ! true
		else drop false then
;

: pick.literal: ( id a n -- )
	2>r 0 swap 2r> pick.oper:
;

 ' + TKN_ADD s" +" pick.oper: pick.op.add?
 ' - TKN_SUB s" -" pick.oper: pick.op.sub?
 ' * TKN_MUL s" *" pick.oper: pick.op.mul?
 ' / TKN_DIV s" /" pick.oper: pick.op.div?
     TKN_LPR s" (" pick.literal: pick.op.lpr?
     TKN_RPR s" )" pick.literal: pick.op.rpr?
     TKN_END s" ;;" pick.literal: pick.end?

: tib-empty? ( -- f )
	>in @ #tib @ >=
;

: lex.next ( <token> -- tkn )
	TKN_END tkn.ref !
	tib-empty? if exit then
	bl word ( a )
	pick.end?    if drop exit then
	pick.number? if drop exit then
	pick.op.add? if drop exit then
	pick.op.sub? if drop exit then
	pick.op.mul? if drop exit then
	pick.op.div? if drop exit then
	pick.op.lpr? if drop exit then
	pick.op.rpr? if drop exit then
	pick.var?    if drop exit then
	TKN_ERR tkn.ref !
	drop
;

: lex.peek? ( tknid -- flag )
	tkn.ref @ =
;

: lex.match ( tknid -- )
	lex.peek?
		if lex.next
		else abort" unknown token"
		then
;


defer exp

: factor
	TKN_LPR lex.peek? if
		TKN_LPR lex.match
		exp
		TKN_RPR lex.match
	else
		TKN_NUM lex.peek? if
			tkn.val @
			TKN_NUM lex.match
		else
			TKN_VAR lex.peek? if
				tkn.val @
				TKN_VAR lex.match
			then
		then
	then
;

: term
	factor
	begin
		TKN_MUL lex.peek? if
			tkn.val @
			TKN_MUL lex.match
		else
			TKN_DIV lex.peek? if
				tkn.val @
				TKN_DIV lex.match
			else
				exit
			then
		then
		factor
		swap execute
	again
;

: exp0 ( <exp> -- )
	term
	begin
		TKN_ADD lex.peek? if
			tkn.val @
			TKN_ADD lex.match
		else
			TKN_SUB lex.peek? if
				tkn.val @
				TKN_SUB lex.match
			else
				exit
			then
		then
		term
		swap execute
	again
;

' exp0 is exp

: :=
	lex.next exp
	swap !
;

decimal
variable ans
variable ans2


ans := 2 * ( 3 + 4 ) * ( 5 + 6 ) ;; ( this is comment )
.( answer = ) ans ? cr

ans2 := ( ans - 30 ) / 10
.( answer2 = ) ans2 ? cr


bye

