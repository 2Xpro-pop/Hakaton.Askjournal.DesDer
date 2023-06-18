class Button extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        let pos = "text-left"
        return (
            <div className="post-block">
                <div className={"post-block-content text-center"} >
                    <div className="row d-flex justify-content-center mt-5">
                        <a href={this.props.data.link} className="btn btn-primary px-5 py-2 mx-auto" style={{ "width": "min - content" }}>
                            {this.props.data.text}
                        </a>
                    </div>
                </div>
            </div>);
    }
}