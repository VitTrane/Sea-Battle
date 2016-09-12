 
var arr = [];
for(i=0; i< 10; i++)
{
	arr[i]=[];
	for(j=0; j< 10; j++)
	{
		arr[i][j] = {status: 'clean', x: j, y: i};
	}
}
//React.render(<Registration/>, document.getElementById('content'));
React.render(<Game/>, document.getElementById('content'));
//React.render(<PlayerField cells={arr}/>, document.getElementById('content'));
//React.render(<Cell status={arr[0][0].status} x={arr[0][0].x} y={arr[0][0].y}/>, document.getElementById('content'));
//React.render(<Row cells={arr[0]}/>,document.getElementById('content'))


