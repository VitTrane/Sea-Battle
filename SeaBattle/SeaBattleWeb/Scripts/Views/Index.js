var st = new GlobalStateCtrl();
st.state = 1;
st.initGame();

ReactDOM.render(React.createElement(App,{stateCtrl: st}), document.getElementById('content'));
