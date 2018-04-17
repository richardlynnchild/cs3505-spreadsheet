#include <gtest/gtest.h>

int simple(int x)
{
  return x;
}

TEST (TestTest, Test1)
{
  ASSERT_EQ (1, simple(1));
  ASSERT_EQ (2, simple(2));
}

int main (int argc, char **argv)
{
  ::testing::InitGoogleTest(&argc, argv);
  return RUN_ALL_TESTS();
}
