''' <summary>
''' Holds the first instance of each form type for easy reference
''' </summary>
''' <remarks></remarks>
Public Class FormController

    ''' <summary>
    ''' Returns a copy of the main MDI parent form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared ReadOnly Property MDIParent() As formMDIParent
        Get
            If IsNothing(_MDIParent) Then
                Throw New Exception("MDI parent form reference is not yet defined!")
            End If

            Return _MDIParent

        End Get
    End Property
    Private Shared WithEvents _MDIParent As formMDIParent

    ''' <summary>
    ''' Sets a reference to the MDI parent form
    ''' </summary>
    ''' <param name="parent"></param>
    ''' <remarks></remarks>
    Public Shared Sub SetMDIParent(ByRef parent As formMDIParent)
        If Not IsNothing(_MDIParent) Then
            Throw New Exception("MDI Parent form has already been defined")
        End If

        _MDIParent = parent

    End Sub



    ''' <summary>
    ''' Returns a copy of the MDI child formBlockCurrent
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared ReadOnly Property CurrentBlocks() As formBlockCurrent
        Get
            If IsNothing(_CurrentBlocks) Then
                _CurrentBlocks = New formBlockCurrent
                _CurrentBlocks.MdiParent = MDIParent
            End If

            Return _CurrentBlocks
        End Get
    End Property
    Private Shared WithEvents _CurrentBlocks As formBlockCurrent


    ''' <summary>
    ''' Returns a copy of the MDI child formBlockHistory
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared ReadOnly Property BlockHistory() As formBlockHistory
        Get
            If IsNothing(_BlockHistory) Then
                _BlockHistory = New formBlockHistory
                _BlockHistory.MdiParent = _MDIParent
            End If

            Return _BlockHistory
        End Get
    End Property
    Private Shared WithEvents _BlockHistory As formBlockHistory


    ''' <summary>
    ''' Returns a copy of the MDI child formOptions
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared ReadOnly Property Options() As formOptions
        Get
            If IsNothing(_Options) Then
                _Options = New formOptions
                _Options.MdiParent = MDIParent
            End If

            Return _Options
        End Get
    End Property
    Private Shared WithEvents _Options As formOptions

End Class
