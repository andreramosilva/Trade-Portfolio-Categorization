Question 2: A new category was created called PEP (politically exposed person). Also, a new bool property
IsPoliticallyExposed was created in the ITrade interface. A trade shall be categorized as PEP if
IsPoliticallyExposed is true. Describe in at most 1 paragraph what you must do in your design to account for this
new category.

To account for the new PEP category, we can modify our design to add a new category class that implements the trade classification logic for PEP trades. This new category class would take into account the IsPoliticallyExposed property of the ITrade interface and classify trades accordingly. We would also need to modify our code that processes the input trades to set the IsPoliticallyExposed property when appropriate. With this design, our system would be extensible to handle any number of additional trade categories, simply by adding new category classes that implement the necessary classification logic.
