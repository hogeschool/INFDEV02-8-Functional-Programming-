module ParseSVM (program, parse, parseTest, Literal(..), Address(..), Location(..), Value(..), Register(..), Instruction(..)) where
import Control.Monad (void)

import Text.Parsec
import Text.Parsec.String -- input stream is of type ‘String’
import Text.Parsec.Language
import qualified Text.Parsec.Token as Token

-- import Text.Megaparsec
-- import Text.Megaparsec.Expr
-- import Text.Megaparsec.String -- input stream is of type ‘String’
-- import qualified Text.Megaparsec.Lexer as L

-- Discriminated union for the 4 registers of the SVM
data Register = Reg1 | Reg2 | Reg3 | Reg4 deriving (Show, Eq)

-- Tuple used to store the line and column index of an instruction. It is used to write the position of a parse or runtime error
-- when the relative exception is thrown
type Position = (Int, Int)

-- Data structures representing the constant values of the language. Address may contain the Integer representing the memory address or
-- the register from which the address is read.
data Literal = Integer Integer -- Position
             | Float Double -- Position
             | String String -- Position
             deriving (Show, Eq)
                      
data Address = Direct Integer | Indirect Register deriving (Show, Eq)

data Location = Address Address | Register Register deriving (Show, Eq)

data Value = Location Location | Literal Literal deriving (Show, Eq)

-- Instructions supported by the SVM. See the documentation for further details.
data Instruction = Nop -- Position
                 | Mov Location Value -- Position
                 | And Register Value -- Position
                 | Or Register Value -- Position
                 | Not Register -- Position
                 | Mod Register Value -- Position
                 | Add Register Value -- Position
                 | Sub Register Value -- Position
                 | Mul Register Value -- Position
                 | Div Register Value -- Position
                 | Cmp Register Value -- Position
                 | Jmp String -- Position
                 | Jc String Register -- Position
                 | Jeq String Register -- Position
                 | Label String -- Position
                   deriving (Show, Eq)

-- The Parser generates a Program data structure which is simply a list of instructions.
type Program = [Instruction]

languageDef =
  emptyDef { Token.commentStart    = "/*"
           , Token.commentEnd      = "*/"
           , Token.commentLine     = "//"
           , Token.identStart      = letter
           , Token.identLetter     = alphaNum <|> char '_'
--           , Token.reservedNames   = []
--           , Token.reservedOpNames = []
           }

lexer = Token.makeTokenParser languageDef

whiteSpace :: Parser ()
whiteSpace = Token.whiteSpace lexer
--whiteSpace = many $ oneOf " \t\n\r\f\v"

lexeme :: Parser a -> Parser a
lexeme = Token.lexeme lexer

symbol :: String -> Parser String
symbol = Token.symbol lexer

brackets :: Parser a -> Parser a
brackets = Token.brackets lexer -- between (symbol "[") (symbol "]")

integer :: Parser Integer
integer = Token.integer lexer

float :: Parser Double
float = Token.float lexer

identifier :: Parser String
identifier = Token.identifier lexer

stringLiteral :: Parser String
stringLiteral = Token.stringLiteral lexer

program :: Parser Program
program = whiteSpace *> programStart <* eof -- between whitespace eof program

-- parse a complete program of instructions separated by newlines
programStart :: Parser Program
programStart = many1 instruction -- sepBy1 instruction (many1 $ lexeme newline)

instruction :: Parser Instruction
instruction = choice $ map try [i_nop, i_mov, i_and, i_or, i_mod, i_add, i_sub, i_mul, i_div, i_cmp, i_label, i_not, i_jmp, i_jc, i_jeq]

i_nop = Nop <$ symbol "nop"

i_mov = Mov <$> (symbol "mov" *> location) <*> value

-- binops
i_and = reg_binop "and" And
i_or = reg_binop "or" Or
i_mod = reg_binop "mod" Mod
i_add = reg_binop "add" Add
i_sub = reg_binop "sub" Sub
i_mul = reg_binop "mul" Mul
i_div = reg_binop "div" Div
i_cmp = reg_binop "cmp" Cmp

reg_binop s r = r <$> (symbol s *> register) <*> value

location = (Register <$> register) <|> (Address <$> address)
value = (Location <$> location) <|> (Literal <$> literal)

literal :: Parser Literal
literal = lexeme $ try (Float <$> float) <|> try (Integer <$> integer) <|> (String <$> stringLiteral)

address = brackets ((Direct <$> integer) <|> (Indirect <$> register))

register = lexeme $ string "reg" *> ((Reg1 <$ char '1') <|> (Reg2 <$ char '2') <|> (Reg3 <$ char '3') <|> (Reg4 <$ char '4'))

i_not = Not <$> (symbol "not" *> register)

i_label = Label <$> (symbol "#" *> identifier )

i_jmp = Jmp <$> (symbol "jmp" *> identifier)
i_jc = label_binop "jc" Jc
i_jeq = label_binop "jeq" Jeq

label_binop s r = r <$> (symbol s *> identifier) <*> register
