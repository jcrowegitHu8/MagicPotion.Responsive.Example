var Modal = ReactBootstrap.Modal;

var LoadingModalView = React.createClass({
	render: function () {


		return (<Modal show={this.props.show}>
			        <div className="modal-content">
				        <div className="modal-body" style={{ textAlign: 'center' }}>
					        <h1>
						        <i style={{ color: '#3498db', fontSize: '1.2em' }}
						           className="fa fa-circle-o-notch fa-spin "></i>&nbsp;<strong>Loading...</strong>
					        </h1>
				        </div>

			        </div>
		        </Modal>);

	}
});