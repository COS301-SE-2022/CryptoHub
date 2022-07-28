var FruitSelector = React.createClass({
  getInitialState: function () {
    return { selectValue: "Radish" };
  },
  handleChange: function (e) {
    this.setState({ selectValue: e.target.value });
  },
  render: function () {
    var message = "You selected " + this.state.selectValue;
    return <div></div>;
  },
});

React.render(<FruitSelector name="World" />, document.body);
