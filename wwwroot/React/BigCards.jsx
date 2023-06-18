class BigCards extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="post-block">
                <div className="post-block-content" >
                    <div
                        className="row align-items-stretch d-flex justify-content-around mt-2 mx-2"
                        style={{ paddingTop: '15px' }} >
                        {this.props.data.cards.map((card) => {
                            return (
                                <div className="col-sm-12 col-md-6 px-sm-1 px-md-4 mt-3">
                                    <div className="tlp-card p-4 text-center text-break">
                                        <h3 className="tlp-inter-600">{card.header}</h3>
                                        <p className="p-2">{card.text}</p>
                                    </div>
                                </div>)
                        })}
                    </div>
                </div>
            </div>);
    }
}