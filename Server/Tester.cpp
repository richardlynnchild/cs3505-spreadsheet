#include <iostream>
#include "spreadsheet.h"
#include "gtest/gtest.h"

using namespace std;

TEST(SpreadsheetConstructor, AssignsName)
{
  Spreadsheet s("mySpreadsheet");
  Spreadsheet t("yourSpreadsheet");
  ASSERT_FALSE(s.GetName() == t.GetName());
}

TEST(SSEditSheet, UpdatesState)
{
  Spreadsheet my_spreadsheet("mySpreadsheet");
  my_spreadsheet.EditSheet("A1", "3");
  std::string val = my_spreadsheet.GetState("A1");
  ASSERT_TRUE(val == "3");
}

TEST(SSEditSheet, UpdatesRevert)
{

}

TEST(SSEditSheet, UpdatesUndo)
{

}

TEST(SSFullState, SimpleFullState)
{
  Spreadsheet s("test");
  s.EditSheet("A6","3");
  s.EditSheet("A9","=A6/2");
  std::string actual = s.GetFullState();
  std::string expected = "full_state A6:3\nA9:=A6/2\n";
  char end = (char) 3;
  expected += end;
  ASSERT_TRUE(expected == actual);
}

int main(int argc, char* argv[])
{
    ::testing::InitGoogleTest(&argc,argv);
    return RUN_ALL_TESTS();
}
