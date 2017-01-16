namespace IUseCaseMakerAddins

open UseCaseMakerLibrary

type ISaveModel =
   // abstract method
   abstract member OnModelSave: Model -> unit