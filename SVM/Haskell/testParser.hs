module Main (main) where

import ParseSVM

-- parses SVM file from standard input port and displays result
main = do
  input <- getContents -- reads from stdin
  let result = parse program "(standard input)" input
  print result
