using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : TrackedCell
{
    public bool isActive() {
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
        if (this.position.x - 1 < 0 || this.position.y - 1 < 0)
            return false;
        if (this.position.x - 1 >= CellFunctions.gridWidth || this.position.y - 1 >= CellFunctions.gridHeight)
            return false;
        if (this.position.x + 1 < 0 || this.position.y + 1 < 0)
            return false;
        if (this.position.x + 1 >= CellFunctions.gridWidth || this.position.y + 1 >= CellFunctions.gridHeight)
            return false;
        //If we don't have a refrence cell return
        /*if (CellFunctions.cellGrid[(int)this.position.x - offsetX, (int)this.position.y - offsetY] == null)
            return false;*/
        return true;
    }

    public override void Step()
    {
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
        if (this.position.x - 1 < 0 || this.position.y - 1 < 0)
            return;
        if (this.position.x - 1 >= CellFunctions.gridWidth || this.position.y - 1 >= CellFunctions.gridHeight)
            return;
        if (this.position.x + 1 < 0 || this.position.y + 1 < 0)
            return;
        if (this.position.x + 1 >= CellFunctions.gridWidth || this.position.y + 1 >= CellFunctions.gridHeight)
            return;

        //If we don't have a refrence cell return
        /*if (CellFunctions.cellGrid[(int)this.position.x - offsetX, (int)this.position.y - offsetY] == null)
            return;*/
        //If there is a cell in our way push it :3
        if (CellFunctions.cellGrid[(int)this.position.x + offsetX, (int)this.position.y + offsetY] != null)
        {
            //if (CellFunctions.cellGrid[(int)this.position.x + offsetX, (int)this.position.y + offsetY].cellType == CellType_e.TRASH)
            //    return;

            (bool, bool) pushResult = CellFunctions.cellGrid[(int)this.position.x + offsetX, (int)this.position.y + offsetY].Push(this.getDirection(), 1);
            if (pushResult.Item2 || !pushResult.Item1)
                return;
        }

        AudioManager.i.PlaySound(GameAssets.i.place);
        Cell newCell1 = GridManager.instance.SpawnCell(
            CellType_e.NUKE,
            new Vector2((int)this.position.x + 1, (int)this.position.y + 0),
            Direction_e.UP,
            true
            );
        newCell1.oldPosition = this.position;
        newCell1.generated = true;
        Cell newCell2 = GridManager.instance.SpawnCell(
            CellType_e.NUKE,
            new Vector2((int)this.position.x - 1, (int)this.position.y - 0),
            Direction_e.UP,
            true
            );
        newCell2.oldPosition = this.position;
        newCell2.generated = true;
        Cell newCell3 = GridManager.instance.SpawnCell(
            CellType_e.NUKE,
            new Vector2((int)this.position.x + 0, (int)this.position.y + 1),
            Direction_e.UP,
            true
            );
        newCell3.oldPosition = this.position;
        newCell3.generated = true;
        Cell newCell4 = GridManager.instance.SpawnCell(
            CellType_e.NUKE,
            new Vector2((int)this.position.x - 0, (int)this.position.y - 1),
            Direction_e.UP,
            true
            );
        newCell4.oldPosition = this.position;
        newCell4.generated = true;
    }
}
