
var ErrorView = React.createClass({
    propTypes: {
        display: React.PropTypes.bool.isRequired
    },
    render() {
        return (this.props.display === true) ? <div>{this.props.children}</div> : null;
    }
});

