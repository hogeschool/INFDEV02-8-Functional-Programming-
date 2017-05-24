module Main (main) where

import ParseSVM

main = do
  input <- getContents
  let result = parse program "" input
  print result
