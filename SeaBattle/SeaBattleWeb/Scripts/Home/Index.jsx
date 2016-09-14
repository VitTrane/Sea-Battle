 
//React.render(<Registration/>, document.getElementById('content'));

//React.render(<PlayField board={board} isReady={false} isPlayer={true} />,document.getElementById('content'));

var board = new Board();
ReactDOM.render(<PlayField board={board} isReady={false}/>,document.getElementById('content'));

//var board = new Board();
//ReactDOM.render(<PlayField board={board} isReady={false} isPlayer={true}/>,document.getElementById('content'));

//ReactDOM.render(<Cell board={board} color={'clean'} row={0} col={0} onPlay={board.play}/>,document.getElementById('content'));


