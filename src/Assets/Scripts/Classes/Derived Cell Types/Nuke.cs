using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : TrackedCell
{
    bool runThisTick = true;

    public bool isActive()
    {
        int offsetX = 0;
        int offsetY = 0;

        switch (this.getDirection())
        {
            case (Direction_e.RIGHT):
                offsetX += 1;
                break;
            case (Direction_e.DOWN):
                offsetY += -1;
                break;
            case (Direction_e.LEFT):
                offsetX += -1;
                break;
            case (Direction_e.UP):
                offsetY += 1;
                break;
        }
        //Array index error prevention
        if (this.position.x + -1 < 0 || this.position.y + -1 < 0)
            return false;
        if (this.position.x + 1 >= CellFunctions.gridWidth || this.position.y + 1 >= CellFunctions.gridHeight)
            return false;
        if (this.position.x + -2 < 0 || this.position.y + -2 < 0)
            return false;
        if (this.position.x + 2 >= CellFunctions.gridWidth || this.position.y + 2 >= CellFunctions.gridHeight)
            return false;

        if (this.position.x < 0 || this.position.y < 0)
            return false;
        if (this.position.x >= CellFunctions.gridWidth || this.position.y >= CellFunctions.gridHeight)
            return false;
        return true;
    }

    public override void Step()
    {
        this.runThisTick = !this.runThisTick;

        //Subract to find refrence, add to find target
        int offsetX = 0;
        int offsetY = 0;

        switch (this.getDirection())
        {
            case (Direction_e.RIGHT):
                offsetX += 1;
                break;
            case (Direction_e.DOWN):
                offsetY += -1;
                break;
            case (Direction_e.LEFT):
                offsetX += -1;
                break;
            case (Direction_e.UP):
                offsetY += 1;
                break;
        }
        //Array index error prevention
        if (this.position.x + -1 < 0 || this.position.y + -1 < 0)
            return;
        if (this.position.x + 1 >= CellFunctions.gridWidth || this.position.y + 1 >= CellFunctions.gridHeight)
            return;

        if (this.position.x < 0 || this.position.y < 0)
            return;
        if (this.position.x >= CellFunctions.gridWidth || this.position.y >= CellFunctions.gridHeight)
            return;
        //If there is a cell in our way push it :3
        if (CellFunctions.cellGrid[(int)this.position.x + -1, (int)this.position.y + 0] != null)
        {
            //if (CellFunctions.cellGrid[(int)this.position.x + offsetX, (int)this.position.y + offsetY].cellType == CellType_e.TRASH)
            //    return;

            (bool, bool) pushResult = CellFunctions.cellGrid[(int)this.position.x + -1, (int)this.position.y + 0].Push(Direction_e.LEFT, 1);
            if (pushResult.Item2 || !pushResult.Item1)
                return;
        }
        else if(CellFunctions.cellGrid[(int)this.position.x + 1, (int) this.position.y + 0] != null)
        {
            //if (CellFunctions.cellGrid[(int)this.position.x + offsetX, (int)this.position.y + offsetY].cellType == CellType_e.TRASH)
            //    return;

            (bool, bool) pushResult = CellFunctions.cellGrid[(int)this.position.x + 1, (int)this.position.y + 0].Push(Direction_e.RIGHT, 1);
            if (pushResult.Item2 || !pushResult.Item1)
                return;
        }
        else if (CellFunctions.cellGrid[(int)this.position.x + 0, (int)this.position.y + 1] != null)
        {
            //if (CellFunctions.cellGrid[(int)this.position.x + offsetX, (int)this.position.y + offsetY].cellType == CellType_e.TRASH)
            //    return;

            (bool, bool) pushResult = CellFunctions.cellGrid[(int)this.position.x + 0, (int)this.position.y + 1].Push(Direction_e.UP, 1);
            if (pushResult.Item2 || !pushResult.Item1)
                return;
        }
        else if (CellFunctions.cellGrid[(int)this.position.x + 0, (int)this.position.y + -1] != null)
        {
            //if (CellFunctions.cellGrid[(int)this.position.x + offsetX, (int)this.position.y + offsetY].cellType == CellType_e.TRASH)
            //    return;

            (bool, bool) pushResult = CellFunctions.cellGrid[(int)this.position.x + 0, (int)this.position.y + -1].Push(Direction_e.DOWN, 1);
            if (pushResult.Item2 || !pushResult.Item1)
                return;
        }

        if (this.runThisTick)
        {
            AudioManager.i.PlaySound(GameAssets.i.place);
            Cell newCellLeft = GridManager.instance.SpawnCell(
                        CellType_e.NUKE,
                        new Vector2(this.position.x + -1, this.position.y + 0),
                        Direction_e.RIGHT,
                        true
                        );
            newCellLeft.oldPosition = this.position;
            newCellLeft.generated = true;

            Cell newCellRight = GridManager.instance.SpawnCell(
                        CellType_e.NUKE,
                        new Vector2(this.position.x + 1, this.position.y + 0),
                        Direction_e.RIGHT,
                        true
                        );
            newCellRight.oldPosition = this.position;
            newCellRight.generated = true;

            Cell newCellUp = GridManager.instance.SpawnCell(
                        CellType_e.NUKE,
                        new Vector2(this.position.x + 0, this.position.y + 1),
                        Direction_e.RIGHT,
                        true
                        );
            newCellUp.oldPosition = this.position;
            newCellUp.generated = true;

            Cell newCellDown = GridManager.instance.SpawnCell(
                        CellType_e.NUKE,
                        new Vector2(this.position.x + 0, this.position.y + -1),
                        Direction_e.RIGHT,
                        true
                        );
            newCellDown.oldPosition = this.position;
            newCellDown.generated = true;

            // print("Tick");
        }
    }
}
