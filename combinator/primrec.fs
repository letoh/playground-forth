 ( a primitive recursion combinator )

: primrec ( n init xt -- n )
	rot 1+ rot tuck 1+ ?do
		i 2 pick execute
	loop nip ;

: fact ( n -- n )
	1 ['] * primrec ;

5 1 ' * primrec . cr
5 fact . cr

bye

