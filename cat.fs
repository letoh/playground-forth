\ 
\ Copyright (C) 2011-2013 Meng-Cheng Cheng (letoh)
\ 
\ Description:
\     cat in forth
\ 
\ author: letoh
\ date: 2011/06/05
\ 
\ run: gforth cat.fs -e bye < input.file
\ 

\ test end of stream
: not-finished ( char -- flag )
	dup 4 <> swap -1 <> and
;

: print ( ch -- )
	emit
;

: from-input ( xt -- )
	begin key dup not-finished while
		over execute
	repeat 2drop
;

' print from-input



