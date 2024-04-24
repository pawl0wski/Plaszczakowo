using GraphDrawer;

namespace ProblemDrawer;

abstract class ProblemDrawerExecutor<TDrawerData> where TDrawerData : GraphData
{
    private List<TDrawerData> _drawerDatas;
}