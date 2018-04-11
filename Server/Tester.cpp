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

int main(int argc, char* argv[])
{
    ::testing::InitGoogleTest(&argc,argv);
    return RUN_ALL_TESTS();
}
