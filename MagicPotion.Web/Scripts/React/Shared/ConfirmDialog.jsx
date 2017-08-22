
var ConfirmDialog = React.createClass({
	propTypes: {
		show: React.PropTypes.bool,
		title: React.PropTypes.string,
		text: React.PropTypes.string,
		confirmButtonText: React.PropTypes.string,
        denyButtonText: React.PropTypes.string,
		confirmButtonClassName: React.PropTypes.string,
		cancelButtonText: React.PropTypes.string,
		onCancel: React.PropTypes.func,
		onConfirm: React.PropTypes.func,
		showDeny: React.PropTypes.bool,
		headerClassNames: React.PropTypes.string
	},
	getCancelButtonText: function() {
		return this.props.cancelButtonText
			? this.props.cancelButtonText
			: "Cancel";
	},
	getDenyButtonText: function() {
		return this.props.denyButtonText
			? this.props.denyButtonText
			: "No";
	},
	getConfirmButtonText: function() {
		return this.props.confirmButtonText
			? this.props.confirmButtonText
			: "OK";
    },
    getConfirmButtonClassName() {
        if (this.props.confirmButtonClassName) {
            return this.props.confirmButtonClassName;
        }
        return "btn btn-primary";
		
	},
	getDisplayStyle: function () {
		return this.props.show ? "block" : "none";
	},
	getDenyStyle: function() {
		return this.props.showDeny ? "" : "none";
    },
    handleConfirm() {
        if (this.props.onConfirm) {
			//eventSource in the original button click that
			//initiated the dialog.
            this.props.onConfirm(this.props.clickSource);
        }
    },

	render: function() {
		return (<div className="modal" style={{display: this.getDisplayStyle(), top: "30%"}} tabIndex="-1" role="dialog">
			        <div className="modal-dialog" role="document">
				        <div className="modal-content">
					        <div className={`modal-header ${this.props.headerClassNames}`}>
						        <button type="button"
						                className="close"
						                data-dismiss="modal"
						                aria-label="Close"
						                onClick={this.props.onCancel}>
							        <span aria-hidden="true">&times;</span>
						        </button>
						        <h4 className="modal-title">{this.props.title}</h4>
					        </div>
					        <div className="modal-body">
						        <p>{this.props.text}</p>
					        </div>
					        <div className="modal-footer">
						        <button
							        type="button"
							        className="btn btn-link"
							        data-dismiss="modal"
							        onClick={this.props.onCancel}>
							        {this.getCancelButtonText()}
						        </button>
						        <button
							        type="button"
							        className="btn btn-outline btn-default"
							        data-dismiss="modal"
							        onClick={this.props.onDeny}
							        style={{display: this.getDenyStyle()}}>
							        {this.getDenyButtonText()}
						        </button>
						        <button
							        type="button"
							        className={this.getConfirmButtonClassName()}
                                    onClick={this.handleConfirm}>
							        {this.getConfirmButtonText()}
						        </button>
					        </div>
				        </div>
			        </div>
		        </div>);
	}
});


