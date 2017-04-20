module FunctionalWeek1

open CommonLatex
open LatexDefinition
open CodeDefinitionLambda
open Interpreter

let LambdaStateTrace(x,y,z) = LambdaStateTrace(x,y,z,true,true,true,true,true,true)
let LambdaCodeBlock(x,y) = LambdaCodeBlock(x,y,false)

let slides =
  [
    Section("Introduction")
    SubSection("Course introduction")
    ItemsBlock
      [
        !"Course topic: what is this course about?"
        !"Examination: how will you be tested?"
        !"Start with course"
      ]

    SubSection("Course topic: functional programming")
    ItemsBlock
      [
        !"Lambda calculus"
        !"From lambda calculus to functional programming"
        !"Functional programming using \Fsharp and Haskell"
      ]

    SubSection("Advantages of functional programming")
    ItemsBlock
      [
        !"Strong mathematical foundations"
        !"Easier to reason about programs"
        !"Parallelism for ``free''"
        !"Correctness guarantees through strong typing (optional)"
      ]    

    SubSection("Examination")
    ItemsBlock
      [
        !"Theory exam: test understanding of theory"
        !"Practical exam: test ability to apply theory in practice"
      ]

    SubSection("Theory exam: reduction and typing")
    ItemsBlock
      [
        !"One question on reduction in lambda calculus"
        ! @"One question on typing in lambda calculus, \Fsharp, or Haskell"
        ! @"\textbf{Passing grade} if both questions answered correctly"
      ]

    SubSection("Practical exam: interpreter for a virtual machine")
    ItemsBlock
      [
        !"In a group, build an interpreter for a virtual machine"
        !"According to a specification that will be provided"
        !"Groups may consist of up to 4 students"
        !"Understanding of code tested \\textbf{individually}"
      ]
      
    SubSection("Lecture topics")
    ItemsBlock
      [
        ! "Semantics(meaning) of imperative languages"
        ! "Lambda calculus, the foundation for functional languages"
      ]

    Section("Semantics of imperative languages")
    SubSection("Imperative program: sequence of statements")
    ItemsBlock //WithTitle "Imperative program: sequence of statements"
      [
        !"Statements directly depend on and alter memory"
        !"Meaning of statements may depend on contents of memory"
        !"Any statement may depend on (read) any memory location"
        !"Any statement may alter any memory location"
      ]

    VerticalStack
      [
        ItemsBlockWithTitle "Example: meaning of statement sequence"
          [
            !"Statement $s_1$ changes the machine state from $S_0$ to $S_1$"
            !"Statement $s_2$ changes the machine state from $S_1$ to $S_2$"
           // !"$(S_0 \stackrel{s_1}{\longmapsto} S_1) \wedge (S_1 \stackrel{s_2}{\longmapsto} S_2) \implies S_0 \stackrel{s_1 s_2}{\longmapsto} S_2$"
            !"Run statement $s_1$, then run statement $s_2$: $s_1 s_2$"
            !"Statement $s_1 s_2$ changes the machine state from $S_0$ to $S_2$"
            
          ]
        !"\centering$(S_0 \stackrel{s_1}{\longmapsto} S_1) \wedge (S_1 \stackrel{s_2}{\longmapsto} S_2) \implies S_0 \stackrel{s_1 s_2}{\longmapsto} S_2$"
 (*       TypingRules
          [
            {
              Premises = [ @"S_0 \stackrel{s_1}{\longmapsto} S_1\quad S_1 \stackrel{s_2}{\longmapsto} S_2" ]
              Conclusion = @"S_0 \stackrel{s_1 s_2}{\longmapsto} S_2"
            }
          ]*)
        Pause
        Question "What about $s_2 s_1$?"

      ]

    VerticalStack
      [
        ItemsBlockWithTitle "Swap order of $s_1 s_2$: $s_2 s_1$"
          [
            !"Sometimes $s_2 s_1$ has the same meaning as $s_1 s_2$\ldots"
            !"Sometimes $s_2 s_1$ is completely different from $s_1 s_2$!"
            !"It depends on $s_1$, $s_2$, and the relevant machine state $S_0$"
            !"It depends on implementation details of $s_1$ and $s_2$"
            !"Implementation details matter $\implies$ leaky abstraction!"
            //!"Often it is impossible to determine because of the implicit dependence via the machine state"
          ]
        Pause
        Question "Can we do better?"
      ]

    VerticalStack
      [
        ItemsBlockWithTitle "Idea for better abstraction: remove implicit dependencies"
          [
            ! @"No implicit dependencies $\implies$ all dependencies explicit"
            !"No access to arbitrary machine state"
            !"Only explicitly-mentioned state may be accessed"
          ]
        Pause
        Question "What if $s_1$ and $s_2$ only read the same state?"
      ]

    VerticalStack
      [
        ItemsBlockWithTitle "What if $s_1{\{x\}}$ and $s_2{\{x\}}$ only read the same state $x$?"
          [
            ! @"$s_1{\{x\}}$ calculates $x+x$, and $s_2{\{x\}}$ calculates the square $x^2$"
          ]
        Question "Can we reorder $s_1{\{x\}}$ and $s_2{\{x\}}$?"
        Pause
        ItemsBlockWithTitle "What if $s_1{\{x\}}$ and $s_2{\{x\}}$ alter the same state $x$?"
          [
            ! @"$s_1{\{x\}}$ sets $x$ to 1, and $s_2{\{x\}}$ sets $x$ to 2"
          ]
        Question "Can we reorder $s_1{\{x\}}$ and $s_2{\{x\}}$?"
        Pause
        Question "Can we do better?"
      ]

    VerticalStack
      [
        ItemsBlockWithTitle "Idea for better abstraction: remove implicit dependencies"
          [
            ! @"No implicit dependencies $\implies$ all dependencies explicit"
            !"No reading if arbitrary machine state"
            !"No mutating of arbitrary machine state"
            !"Only explicitly-mentioned machine state may be read"
          ]
        Pause
        Block !"NB: No provision at all is made for mutating machine state"
        ]

    VerticalStack
      [
        ItemsBlockWithTitle "Wait a minute, this is just like functions"
          [
            !"Not statements, but (mathematical) functions"
            !"Functions depend only on arguments"
            !"Functions do not alter state"
            !"Can calculate function value when all arguments are known"
            !"Can always replace a function call by its value"
          ]
       ]

    VerticalStack
      [
        BlockWithTitle(@"\textbf{Referential transparency:}", !"It is always valid to replace a function call by its value")
        Pause
        BlockWithTitle("Advanced topic:", !"Allow mutation of state without losing referential transparency")
      ]

    Section "Lambda calculus"
    SubSection "Introduction to Lambda Calculus"
    VerticalStack
      [
        ItemsBlockWithTitle "What is lambda calculus?"
          [
            !"Model of computation based on functions"
            !"Completely different from Turing machines, but equivalent"
            !"Foundation of all functional programming languages"
            !"Truly tiny when compared with its power"
            !"Consists of only (function) abstraction and application"
          ]
      ]

    SubSection "Substitution principle"
    ItemsBlock 
      [
        ! @"The (basic) lambda calculus is truly tiny when compared with its power."
        ! @"It is based on the substitution principle: calling a function with some parameters returns the function body with the variables replaced."
        ! @"There is no memory and no program counter: all we need to know is stored inside the body of the program itself."
      ]    

    SubSection "Grammar"
    VerticalStack
      [
        ItemsBlockWithTitle "A lambda calculus term is one of three things:"
          [
            !"a variable (from some arbitrary infinite set of variables)"
            !"an abstraction (a ``function of one variable'')"
            !"an application (a ``function call'')"
          ]
      ]

    VerticalStack
      [
        BlockWithTitle("Variables (arbitrary infinite set):",! @"$a, b, c, \ldots\hfil a_0, a_1, \ldots\hfil b_0, b_1, \ldots$")
        BlockWithTitle("Abstractions:",! @"For any variable $x$ and lambda term $T$: $\lambda x.T$")
        BlockWithTitle("Applications:",! @"For any lambda terms $F$ and $T$: $(FT)$")
      ]
    VerticalStack
      [
        ItemsBlock
          [
            ! @"Infinite set of variables: $x_0, x_1, \ldots, y_0, y_1, \ldots$, etc."
            ! @"Abstractions (function declarations with one parameter): $\lambda x\rightarrow t$ where $x$ is a variable and $t$ is the function body (a program)."
            ! @"Applications (function calls with one argument): $t\ u$ where $t$ is the function being called (a program) and $u$ is its argument (another program)."
          ]
      ]

    VerticalStack
      [
        TextBlock "A simple example would be the identity function, which just returns whatever it gets as input"

        LambdaCodeBlock(TextSize.Small, -"x" ==> !!"x")
      ]

    VerticalStack
      [
        TextBlock "We can call this function with a variable as argument, by writing:"

        LambdaCodeBlock(TextSize.Small, (-"x" ==> !!"x") >> !!"v")
      ]

    SubSection "Beta reduction"
    VerticalStack
      [
        TextBlock @"A lambda calculus program is computed by replacing lambda abstractions applied to arguments with the body of the lambda abstraction with the argument instead of the lambda parameter:"

        Pause

        TypingRules
          [
            {
              Premises = []
              Conclusion = @"(\lambda x \rightarrow t)\ u  \rightarrow_\beta t [ x \mapsto u ]"
            }
          ]

        TextBlock @"$t [ x \mapsto u ]$ means that we change variable $x$ with $u$ within $t$"
      ]

    LambdaStateTrace(TextSize.Small, (-"x" ==> !!"x") >> !!"v", Option.None)

    VerticalStack
      [
        TextBlock @"Multiple applications where the left-side is not a lambda abstraction are solved in a left-to-right fashion:"

        Pause

        TypingRules
          [
            {
              Premises = [ @"t \rightarrow_\beta t'"; @"u \rightarrow_\beta u'"; @"(t'u') \rightarrow_\beta v"]
              Conclusion = @"(t u) \rightarrow_\beta v"
            }
          ]
      ]

    VerticalStack
      [
        TextBlock @"Variables cannot be further reduced, that is they stay the same:"

        Pause

        TypingRules
          [
            {
              Premises = []
              Conclusion = @"x \rightarrow_\beta x"
            }
          ]
      ]

    SubSection "Multiple parameters"
    VerticalStack
      [
        TextBlock "We can encode functions with multiple parameters by nesting lambda abstractions:"

        LambdaCodeBlock(TextSize.Small, -"x" ==> (-"y" ==> (!!"x" >> !!"y")))
      ]

    VerticalStack
      [
        TextBlock "The parameters are then given one at a time:"

        LambdaCodeBlock(TextSize.Small, ((-"x" ==> (-"y" ==> (!!"x" >> !!"y"))) >> !!"A") >> !!"B")
      ]

    LambdaStateTrace(TextSize.Small, ((-"x" ==> (-"y" ==> (!!"x" >> !!"y"))) >> !!"A") >> !!"B", Option.None)

    Section "Closing up"
    SubSection "Example executions of (apparently) nonsensical programs"
    ItemsBlock
      [
        ! @"We will now exercise with the execution of various lambda programs."
        ! @"Try to guess what the result of these programs is, and then we shall see what would have happened."
      ]

    VerticalStack
      [
        Question "What is the result of this program execution?"

        LambdaCodeBlock(TextSize.Small, ((-"x" ==> (-"y" ==> (!!"x" >> !!"y")) >> (-"z" ==> (!!"z" >> !!"z"))) >> !!"A"))
      ]

    LambdaStateTrace(TextSize.Small, ((-"x" ==> (-"y" ==> (!!"x" >> !!"y")) >> (-"z" ==> (!!"z" >> !!"z"))) >> !!"A"), Option.None)

    VerticalStack
      [
        Question "What is the result of this program execution? Watch out for the scope of the two ``x'' variables!"

        LambdaCodeBlock(TextSize.Small, (-"x" ==> (-"x" ==> (!!"x" >> !!"x")) >> !!"A") >> !!"B")
      ]

    LambdaStateTrace(TextSize.Small, (-"x" ==> (-"x" ==> (!!"x" >> !!"x")) >> !!"A") >> !!"B", Option.None)

    VerticalStack
      [
        TextBlock "The first ``x'' gets replaced with ``A'', but the second ``x'' shadows it!"

        LambdaCodeBlock(TextSize.Small, (-"x" ==> (-"x" ==> (!!"x" >> !!"x")) >> !!"A") >> !!"B")
      ]

    VerticalStack
      [
        TextBlock "A better formulation, less ambiguous, would turn:"

        LambdaCodeBlock(TextSize.Small, (-"x" ==> (-"x" ==> (!!"x" >> !!"x")) >> !!"A") >> !!"B")

        TextBlock "...into:"

        LambdaCodeBlock(TextSize.Small, (-"y" ==> (-"x" ==> (!!"x" >> !!"x")) >> !!"A") >> !!"B")
      ]

    LambdaStateTrace(TextSize.Small, (-"y" ==> (-"x" ==> (!!"x" >> !!"x")) >> !!"A") >> !!"B", Option.None)


    VerticalStack
      [
        Question "What is the result of this program execution? Is there even a result?"

        LambdaCodeBlock(TextSize.Small, (-"x" ==> (!!"x" >> !!"x")) >> (-"x" ==> (!!"x" >> !!"x")))
      ]

    LambdaStateTrace(TextSize.Small, (-"x" ==> (!!"x" >> !!"x")) >> (-"x" ==> (!!"x" >> !!"x")), Some 2)

    VerticalStack
      [
        LambdaCodeBlock(TextSize.Small, (-"x" ==> (!!"x" >> !!"x")) >> (-"x" ==> (!!"x" >> !!"x")))

        TextBlock @"It never ends! Like a \texttt{while true: ..}!"
      ]

    SubSection "Crazy teachers tormenting poor students, or ``where are my integers?''"
    VerticalStack
      [
        TextBlock "Ok, I know what you are all thinking: what is this for sick joke? This is no real programming language!"

        ItemsBlock
          [
            ! "We have some sort of functions and function calls"
            ! @"We do not have booleans and \textttt{if}'s"
            ! @"We do not have integers and arithmetic operators"
            ! @"We do not have a lot of things!"
          ]
      ]

    SubSection "Surprise!"
    TextBlock "With nothing but lambda programs we will show how to build all of these features and more."

    SubSection "Stay tuned."
    TextBlock "This will be a marvelous voyage."
  ]

