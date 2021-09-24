import { TiledMapUI } from "../game/view/TiledMapUI";

var mapMaze: any[]; //场景节点顺序保存数组
var openTable: any[]; //开启列表
var closeTable: any[]; //关闭列表
var pathStack: any[]; //保存路径

var openNodeCount = 0; //open列表长度
var closeNodeCount = 0;  //close列表长度
var pathLen = 0;  //路径长度（指针）
var _endPoint: Laya.Point; //结束地图索引
var isFound = false; //是否找到路径

export class AStartFindPath {
    public static AStartFindPath(start, end) {
        let path = [];
        _endPoint = new Laya.Point(0, 0);

        if (TiledMapUI.metaGetTiledData(end.x, end.y) > 0) {
            return path;
        } else {
            let startpoint = TiledMapUI.getPosPoint({ x: start.x, y: start.y });

            _endPoint.x = end.x;
            _endPoint.y = end.y;

            startpoint.x = Math.min(Math.max(0, Math.floor(startpoint.x)), TiledMapUI._width);
            startpoint.y = Math.floor(startpoint.y);

            if (_endPoint.x == startpoint.x && startpoint.y == _endPoint.y) {
                return path;
            } else {

                //初始化map
                this.astartInit();

                //open列表是一个待检查的方格列表
                let start_node = mapMaze[startpoint.y + startpoint.x * TiledMapUI._height];
                let end_node = mapMaze[_endPoint.y + _endPoint.x * TiledMapUI._height];
                let curr_node = null;
                if (start_node == undefined) {
                    console.log(start);
                }

                openTable[openNodeCount++] = start_node;
                start_node.s_is_in_opentable = true;
                start_node.s_g = 0;

                //哈曼顿距离
                start_node.s_h = Math.abs(end_node.s_x - start_node.s_x) + Math.abs(end_node.s_y - start_node.s_y);
                start_node.s_parent = null;

                isFound = false;

                while (true) {
                    this.findMinOfPoint();
                    curr_node = openTable[0]; //第一个点一定是f值最小的点

                    openTable.shift();
                    openNodeCount--;

                    this.adjust_heap(0);

                    closeTable[closeNodeCount++] = curr_node; //当前点加入close列表

                    curr_node.s_is_in_closetable = true;

                    if (curr_node.s_x == end_node.s_x && curr_node.s_y == end_node.s_y) {
                        isFound = true;
                        break;
                    }

                    this.getNeighbor(curr_node, end_node);

                    if (openNodeCount == 0) {
                        isFound = false;
                        break;
                    }

                }
                if (isFound) {
                    curr_node = end_node;
                    while (curr_node) {
                        pathStack[++pathLen] = curr_node;
                        curr_node = curr_node.s_parent;
                    }

                    while (pathLen > 0) {
                        let p = TiledMapUI.getPointPos({ x: pathStack[pathLen].s_x, y: pathStack[pathLen].s_y });
                        path.push([p.x, p.y]);

                        pathLen--;

                    }
                } else {
                    console.log("没有找到路径");
                }
                return path;
            }

        }
    }

    //初始化meta_map
    private static astartInit() {
        openTable = [];
        closeTable = [];
        pathStack = [];
        mapMaze = [];

        isFound = false;
        openNodeCount = 0;
        closeNodeCount = 0;
        pathLen = 0;

        for (let i = 0; i < TiledMapUI._width; i++) {
            for (let j = 0; j < TiledMapUI._height; j++) {
                let node = {
                    s_g: 0,
                    s_h: 0,
                    s_is_in_closetable: false,
                    s_is_in_opentable: false,
                    s_style: TiledMapUI.metaGetTileData(i, j), //0, >0
                    s_x: i,
                    s_y: j,
                    s_parent: null
                };

                mapMaze.push(node);

                // path.push(null);
                // openTable.push(null);
                // closeTable.push(null);
            }
        }
    }

    //添加到openList
    private static insert_to_openlist(x, y, curr_node, end_node, w) {
        // let i;

        if (mapMaze[x * TiledMapUI._height + y].s_style == 0) {
            if (!mapMaze[x * TiledMapUI._height + y].s_is_in_closetable) {
                if (mapMaze[x * TiledMapUI._height + y].s_is_in_opentable) {
                    //需要判断是否是一条更优化的路径
                    //检查如果用新的路径到达（经过 C 的路径） G值是否会更低一些
                    //如果新的G值更低，就把它的 “父方格” 改为目前选中的方格 C
                    //然后重新计算它的 F 值和 G 值（对于每个方块 H 值是不变的）
                    //如果新的 G 值比较高，就说明经过 C 再到达 D 不是一个明智的选择
                    if (mapMaze[x * TiledMapUI._height + y].s_g > curr_node.s_g + w) {
                        mapMaze[x * TiledMapUI._height + y].s_g = curr_node.s_g + w;
                        mapMaze[x * TiledMapUI._height + y].s_parent = curr_node;

                        // for (i = 0; i < openNodeCount; i++) {
                        //     if (openTable[i].s_x == mapMaze[x * _height + y].s_x && openTable[i].s_y == mapMaze[x * _height + y].y) {
                        //         break
                        //     }
                        // }
                        // TiledMapUI.adjust_heap(i)

                        // TiledMapUI.findMinOfPoint()
                    }
                } else {
                    mapMaze[x * TiledMapUI._height + y].s_g = curr_node.s_g + w;
                    mapMaze[x * TiledMapUI._height + y].s_h = Math.abs(end_node.s_x - x) + Math.abs(end_node.s_y - y);
                    mapMaze[x * TiledMapUI._height + y].s_parent = curr_node;
                    mapMaze[x * TiledMapUI._height + y].s_is_in_opentable = true;
                    openTable[openNodeCount++] = mapMaze[x * TiledMapUI._height + y];
                }
            }
        }
    }

    //检查邻居
    private static getNeighbor(curr_node, end_node) {
        let x = curr_node.s_x;
        let y = curr_node.s_y;

        //直线损耗10，斜线损耗14
        if ((x + 1) >= 0 && (x + 1) < TiledMapUI._width && y >= 0 && y < TiledMapUI._height) {
            this.insert_to_openlist(x + 1, y, curr_node, end_node, 10);
        }

        if ((x - 1) >= 0 && (x - 1) < TiledMapUI._width && y >= 0 && y < TiledMapUI._height) {
            this.insert_to_openlist(x - 1, y, curr_node, end_node, 10);
        }

        if (x >= 0 && x < TiledMapUI._width && (y + 1) >= 0 && (y + 1) < TiledMapUI._height) {
            this.insert_to_openlist(x, y + 1, curr_node, end_node, 10);
        }

        if (x >= 0 && x < TiledMapUI._width && (y - 1) >= 0 && (y - 1) < TiledMapUI._height) {
            this.insert_to_openlist(x, y - 1, curr_node, end_node, 10);
        }

        // if ((x + 1) >= 0 && (x + 1) < _width && (y + 1) >= 0 && (y + 1) < _height) {
        //     this.insert_to_openlist(x + 1, y + 1, curr_node, end_node, 10 + 4);
        // }

        // if ((x + 1) >= 0 && (x + 1) < _width && (y - 1) >= 0 && (y - 1) < _height) {
        //     this.insert_to_openlist(x + 1, y - 1, curr_node, end_node, 10 + 4);
        // }

        // if ((x - 1) >= 0 && (x - 1) < _width && (y + 1) >= 0 && (y + 1) < _height) {
        //     this.insert_to_openlist(x - 1, y + 1, curr_node, end_node, 10 + 4);
        // }

        // if ((x - 1) >= 0 && (x - 1) < _width && (y - 1) >= 0 && (y - 1) < _height) {
        //     this.insert_to_openlist(x - 1, y - 1, curr_node, end_node, 10 + 4);
        // }
    }

    //调整堆
    private static adjust_heap(nIndex) {
        let curr = nIndex;
        let child = curr * 2 + 1; //得到左孩子
        var s_parent = Math.floor((curr - 1) / 2);
        if (nIndex < 0 || nIndex >= openNodeCount) { return; }

        while (child < openNodeCount) {
            if (child + 1 < openNodeCount && openTable[child].s_g + openTable[child].s_h > openTable[child + 1].s_g + openTable[child + 1].s_h) {
                child++;
            }
            if (openTable[curr].s_g + openTable[curr].s_h <= openTable[child].s_g + openTable[child].s_h) {
                break;
            } else {
                this.swap(child, curr); //交换节点
                curr = child;  //再判断当前孩子节点
                child = curr * 2 + 1; // 再判断左孩子
            }
        }

        if (curr != nIndex) {
            return;
        }

        while (curr != 0) {
            if (openTable[curr].s_g + openTable[curr].s_h >= openTable[s_parent].s_g + openTable[s_parent].s_h) {
                break;
            } else {
                this.swap(curr, s_parent);
                curr = s_parent;
                s_parent = Math.floor((curr - 1) / 2);
            }
        }

    }

    //找邻居里最小的点
    private static findMinOfPoint() {
        for (let i = 0; i < openNodeCount - 1; i++) {
            if (openTable[i].s_g + openTable[i].s_h > openTable[i + 1].s_g + openTable[i + 1].s_h) {

                this.swap(openTable[i], openTable[i + 1]);
            }
        }
    }

    private static swap(x, y) {
        let temp = x;
        x = y;
        y = temp;
    }
}