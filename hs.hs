len :: [a] -> Integer

len[] = 0
len(x:xs) =1 + len xs