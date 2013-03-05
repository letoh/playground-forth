\ 
\ Copyright (C) 2013 Meng-Cheng Cheng (letoh)
\ 
\ Description:
\     test for word structure and meta programming (gforth)
\ 

 ( 

see .name
: id.
  name>string type space ; ok
see name>string
: name>string
  cell+ dup cell+ swap @ lcount-mask and ; ok
lcount-mask  ok
.s <1> 536870911  ok
hex  ok
.s <1> 1FFFFFFF  ok


: abcd
    ." xt: " here u. cr
    create
    ." xt2: " here u. cr
    does>
        ." xt3: " here u. cr
;  ok

' abcd u. cr B7A98C88
 ok
abcd efgh xt: B7A98D74
xt2: B7A98D88
 ok
' efgh u. B7A98D80  ok
efgh xt3: B7A98D88
 ok

 )

: .parser-name ( a -- a )
    dup @ ( addr of length )
    dup cell+ ( str )
    swap @ 255 and ( a len )
    ." parse " type ." : "
;

: terminal: ( <name> -- )
    here
    create
    cell+ , \ store address of word name

    does> ( <name> -- num flag )
        .parser-name
        drop
;

terminal: num

num


 (
show-word: abcd:
   name: B7A99108
   xt  : B7A99118
   body: B7A99120

create new word at: B7A99330
  name: efgh
  body: B7A99348

show-word: efgh
   name: B7A99330
   xt  : B7A99340
   body: B7A99348

exec new word: efgh
    body: B7A99348
    xt4: B7A99348
end

 )


cr .( test... ) cr

: show-word ( xt -- )
    ." show-word: " dup >name .name cr
    ."    name: " dup >name u. cr
    ."    xt  : " dup u. cr
    ."    body: " dup >body u. cr
    drop cr
;

: abcd:
    ." create new word at: " here u. cr
    create
    ."   name: " here body> >name .name cr
    ."   body: " here u. cr
    cr
    does>
        ." exec new word: " here body> >name .name cr
        ."     body: " here u. cr
        ."     xt4: " u. cr
        ." end" cr
;

hex

' abcd: show-word

abcd: efgh

' efgh show-word

efgh


bye

