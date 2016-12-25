<?xml version="1.0" encoding="utf-8" ?> 
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="xml" version="1.0" encoding="utf-8" standalone="yes"/>

  <!-- Application parameters -->
  <xsl:param name="version"/>
  <xsl:param name="outputType"/>
  <!-- General parameters -->
  <xsl:param name="description"/>
  <xsl:param name="notes"/>
  <xsl:param name="relatedDocs"/>
  <xsl:param name="model"/>
  <xsl:param name="actor"/>
  <xsl:param name="useCase"/>
  <xsl:param name="package"/>
  <xsl:param name="actors"/>
  <xsl:param name="useCases"/>
  <xsl:param name="packages"/>
  <xsl:param name="summary"/>
  <xsl:param name="glossary"/>
  <xsl:param name="glossaryItem"/>
  <xsl:param name="stakeholders"/>
  <xsl:param name="stakeholder"/>
  <!-- 'Actor' specific parameters -->
  <xsl:param name="goals"/>
  <!-- 'Use case' specific paramters -->
  <xsl:param name="preconditions"/>
  <xsl:param name="postconditions"/>
  <xsl:param name="triggerEvent"/>
  <xsl:param name="triggerDescription"/>
  <xsl:param name="activeActors"/>
  <xsl:param name="primary"/>
  <xsl:param name="details"/>
  <xsl:param name="level"/>
  <xsl:param name="status"/>
  <xsl:param name="complexity"/>
  <xsl:param name="priority"/>
  <xsl:param name="implementation"/>
  <xsl:param name="release"/>
  <xsl:param name="assignedTo"/>
  <xsl:param name="openIssues"/>
  <xsl:param name="flowOfEvents"/>
  <xsl:param name="prose"/>
  <xsl:param name="history"/>
  <xsl:param name="implementationNodeSet"/>
  <xsl:param name="statusNodeSet"/>
  <xsl:param name="complexityNodeSet"/>
  <xsl:param name="levelNodeSet"/>
  <xsl:param name="historyTypeNodeSet"/>
  <xsl:param name="eventTypeNodeSet"/>
  <!-- Requirements specific parameters -->
  <xsl:param name="requirements" />
  <xsl:param name="categoryNodeSet" />
  <xsl:param name="category" />
  <xsl:param name="importanceNodeSet" />
  <xsl:param name="importance" />
  <xsl:param name="acceptanceNodeSet" />
  <xsl:param name="acceptance" />
  <xsl:param name="proposedBy" />
  <xsl:param name="benefitTo" />
  <xsl:param name="mappedUCs" />
  <!-- Cover page specific parameters -->
  <xsl:param name="author" />
  <xsl:param name="company" />
  <xsl:param name="creationDate" />
  <xsl:param name="exportPrintDate" />
  <xsl:param name="now" />  

  <xsl:include href="PdfRtfStyles.xsl" />
  
  <!-- Start of template -->
  <xsl:template match="/">
    <itext pagesize="A4" orientation="portrait" left="50" top="50" right="50" bottom="50">
      <xsl:apply-templates/>
    </itext>
  </xsl:template>
  
  <!-- Start of Summary section -->
  <!-- Summary section removed in version 0.9.3 -->
  <!--
  <xsl:template name="MakeSummary">
    <paragraph xsl:use-attribute-sets="ParaTitle">
	  <xsl:value-of select="$summary"/>
    </paragraph>
    <newline/>
    <xsl:call-template name="MakeSummaryItem">
      <xsl:with-param name="node" select="//Model"/>
      <xsl:with-param name="indent" select="0"/>
    </xsl:call-template>
  </xsl:template>

  <xsl:template name="MakeSummaryItem">
    <xsl:param name="node"/>
    <xsl:param name="indent"/>
    <xsl:call-template name="FormatSummaryItem">
      <xsl:with-param name="node" select="$node"/>
      <xsl:with-param name="indent" select="$indent"/>
    </xsl:call-template>
    <xsl:for-each select="$node/Actors/Actor">
      <xsl:call-template name="MakeSummaryItem">
        <xsl:with-param name="node" select="."/>
        <xsl:with-param name="indent" select="$indent + 20.0"/>
      </xsl:call-template>
    </xsl:for-each>
    <xsl:for-each select="$node/UseCases/UseCase">
      <xsl:call-template name="MakeSummaryItem">
        <xsl:with-param name="node" select="."/>
        <xsl:with-param name="indent" select="$indent + 20.0"/>
      </xsl:call-template>
    </xsl:for-each>
    <xsl:for-each select="$node/Packages/Package">
      <xsl:call-template name="MakeSummaryItem">
        <xsl:with-param name="node" select="."/>
        <xsl:with-param name="indent" select="$indent + 20.0"/>
      </xsl:call-template>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="FormatSummaryItem">
    <xsl:param name="node"/>
    <xsl:param name="indent"/>
	<xsl:element name="paragraph">
      <xsl:attribute name="indentationleft">
        <xsl:value-of select="$indent" />
      </xsl:attribute>
      <xsl:attribute name="spacingafter">10.0</xsl:attribute>
        <xsl:element name="anchor" use-attribute-sets="ElementLink">
          <xsl:attribute name="reference">
            <xsl:value-of select="concat('#',$node/@UniqueID)"/>
          </xsl:attribute>
          <xsl:value-of select="concat($node/@Name,' (',$node/@Prefix,$node/@ID,') ')"/>
      </xsl:element>
	</xsl:element>
  </xsl:template>
  -->
  <!-- End of Summary section -->   
   
    <!-- Start of Glossary section -->
  <xsl:template match="Glossary">
    <paragraph xsl:use-attribute-sets="ParaTitle">
      <xsl:value-of select="$glossary"/>
    </paragraph>
    <table columns="2" width="100%" widths="33.33333;66.66666" cellpadding="2.0" cellsfitpage="true">
      <row>
        <cell xsl:use-attribute-sets="ElementTitleCell">
          <paragraph xsl:use-attribute-sets="ElementTitle">
             <xsl:value-of select="$glossaryItem"/>
          </paragraph>
        </cell>
        <cell xsl:use-attribute-sets="ElementTitleCell">
          <paragraph xsl:use-attribute-sets="ElementTitle">
            <xsl:value-of select="$description"/>
          </paragraph>
        </cell>
      </row>
      <xsl:apply-templates select="GlossaryItem"/>
    </table>
  </xsl:template>
  <xsl:template match="GlossaryItem">
    <row>
      <cell xsl:use-attribute-sets="ElementTextCell">
        <xsl:choose>
          <xsl:when test="$outputType = 'withLink'">
            <xsl:element name="anchor" use-attribute-sets="ElementText">
              <xsl:attribute name="name">
                <xsl:value-of select="@UniqueID"/>
              </xsl:attribute>
              <xsl:value-of select="@Name"/>
           </xsl:element>
          </xsl:when>
          <xsl:otherwise>
            <paragraph xsl:use-attribute-sets="ElementText">
              <xsl:value-of select="@Name"/>
            </paragraph>
          </xsl:otherwise>
        </xsl:choose>
      </cell>
      <cell xsl:use-attribute-sets="ElementTextCell">
        <paragraph xsl:use-attribute-sets="ElementText">
          <xsl:call-template name="MatchLink">
            <xsl:with-param name="text" select="Description"/>
          </xsl:call-template>
        </paragraph>
      </cell>
    </row>
  </xsl:template>
  <!-- End of Glossary section -->

  <!-- Start of Stakeholders section -->
  <xsl:template match="Stakeholders">
    <paragraph xsl:use-attribute-sets="ParaTitle">
      <xsl:value-of select="$stakeholders"/>
    </paragraph>
    <table columns="2" width="100%" widths="33.33333;66.66666" cellpadding="2.0" cellsfitpage="true">
      <row>
        <cell xsl:use-attribute-sets="ElementTitleCell">
          <paragraph xsl:use-attribute-sets="ElementTitle">
            <xsl:value-of select="$stakeholder"/>
          </paragraph>
        </cell>
        <cell xsl:use-attribute-sets="ElementTitleCell">
          <paragraph xsl:use-attribute-sets="ElementTitle">
            <xsl:value-of select="$description"/>
          </paragraph>
        </cell>
      </row>
      <xsl:apply-templates select="Stakeholder"/>
    </table>
  </xsl:template>
  <xsl:template match="Stakeholder">
    <row>
      <cell xsl:use-attribute-sets="ElementTextCell">
        <xsl:choose>
          <xsl:when test="$outputType = 'withLink'">
            <xsl:element name="anchor" use-attribute-sets="ElementText">
              <xsl:attribute name="name">
                <xsl:value-of select="@UniqueID"/>
              </xsl:attribute>
              <xsl:value-of select="@Name"/>
            </xsl:element>
          </xsl:when>
          <xsl:otherwise>
            <paragraph xsl:use-attribute-sets="ElementText">
              <xsl:value-of select="@Name"/>
            </paragraph>
          </xsl:otherwise>
        </xsl:choose>
      </cell>
      <cell xsl:use-attribute-sets="ElementTextCell">
        <paragraph xsl:use-attribute-sets="ElementText">
          <xsl:call-template name="MatchLink">
            <xsl:with-param name="text" select="Description"/>
          </xsl:call-template>
        </paragraph>
      </cell>
    </row>
  </xsl:template>
  <!-- End of Stakeholders section -->  

  <!-- Model -->
  <xsl:template match="Model">
    <!-- Cover page -->
    <newline/>
    <newline/>
    <newline/>
    <newline/>
    <newline/>
    <newline/>
    <newline/>
    <newline/>
    <newline/>
    <newline/>
    <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
      <row>
        <cell xsl:use-attribute-sets="CoverCell">
          <paragraph xsl:use-attribute-sets="CoverText">
            <xsl:value-of select="$model"/>
          </paragraph>
        </cell>
      </row>
      <row>
        <cell xsl:use-attribute-sets="CoverCell">
          <paragraph xsl:use-attribute-sets="CoverTitle">
            <xsl:value-of select="@Name"/>
          </paragraph>
        </cell>
      </row>
    </table>
    <paragraph xsl:use-attribute-sets="ParaSep"/>
    <xsl:if test="@Author != ''">
      <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
        <row>
          <cell xsl:use-attribute-sets="CoverCell">
            <paragraph xsl:use-attribute-sets="CoverText">
              <xsl:value-of select="$author"/>
            </paragraph>
          </cell>
        </row>
        <row>
          <cell xsl:use-attribute-sets="CoverCell">
            <paragraph xsl:use-attribute-sets="CoverSubTitle">
              <xsl:value-of select="@Author"/>
            </paragraph>
          </cell>
        </row>
      </table>
      <paragraph xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <xsl:if test="@Company != ''">
      <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
        <row>
          <cell xsl:use-attribute-sets="CoverCell">
            <paragraph xsl:use-attribute-sets="CoverText">
              <xsl:value-of select="$company"/>
            </paragraph>
          </cell>
        </row>
        <row>
          <cell xsl:use-attribute-sets="CoverCell">
            <paragraph xsl:use-attribute-sets="CoverSubTitle">
              <xsl:value-of select="@Company"/>
            </paragraph>
          </cell>
        </row>
      </table>
      <paragraph xsl:use-attribute-sets="ParaSep"/>
      <paragraph xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <xsl:if test="@Release != ''">
      <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
        <row>
          <cell xsl:use-attribute-sets="CoverCell">
            <paragraph xsl:use-attribute-sets="CoverText">
              <xsl:value-of select="$release"/><xsl:value-of select="concat(': ',@Release)"/>
            </paragraph>
          </cell>
        </row>
      </table>
      <paragraph xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
      <row>
        <cell xsl:use-attribute-sets="CoverCell">
          <paragraph xsl:use-attribute-sets="CoverText">
            <xsl:value-of select="$creationDate"/>: <xsl:value-of select="substring-before(@CreationDateValue,' ')"/>
          </paragraph>
        </cell>
      </row>
      <row>
        <cell xsl:use-attribute-sets="CoverCell">      
          <paragraph xsl:use-attribute-sets="CoverText">
            <xsl:value-of select="$exportPrintDate"/>: <xsl:value-of select="substring-before($now,' ')"/>
          </paragraph>
        </cell>
      </row>
    </table>
    <newpage/>
	  <!-- Common attributes -->
    <xsl:call-template name="CommonAttributes">
      <xsl:with-param name="node" select="."/>
      <xsl:with-param name="elementType" select="$model"/>
    </xsl:call-template>
    <paragraph xsl:use-attribute-sets="ParaSep"/>
    <!-- Actors list -->
    <xsl:if test="Actors/*">
      <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="text" select="$actors"/>
        </xsl:call-template>
        <xsl:for-each select="Actors/Actor">
          <xsl:call-template name="IntLinkRow">
            <xsl:with-param name="text" select="concat(@Name,' (',@Prefix,@ID,')')"/>
            <xsl:with-param name="link" select="@UniqueID"/>
          </xsl:call-template>
        </xsl:for-each>
      </table>
      <paragraph xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <!-- Use cases list -->
    <xsl:if test="UseCases/*">
      <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="text" select="$useCases"/>
        </xsl:call-template>
        <xsl:for-each select="UseCases/UseCase">
          <xsl:call-template name="IntLinkRow">
            <xsl:with-param name="text" select="concat(@Name,' (',@Prefix,@ID,')')"/>
            <xsl:with-param name="link" select="@UniqueID"/>
          </xsl:call-template>
        </xsl:for-each>
      </table>
      <paragraph xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <!-- Packages list -->
    <xsl:if test="Packages/*">
      <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="text" select="$packages"/>
        </xsl:call-template>
        <xsl:for-each select="Packages/Package">
          <xsl:call-template name="IntLinkRow">
            <xsl:with-param name="text" select="concat(@Name,' (',@Prefix,@ID,')')"/>
            <xsl:with-param name="link" select="@UniqueID"/>
          </xsl:call-template>
        </xsl:for-each>
      </table>
      <paragraph xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <!-- Requirements details -->
    <xsl:if test="Requirements/*">
      <xsl:apply-templates select="Requirements"/>
      <newpage/>
    </xsl:if>
    <!-- Actors details -->
    <xsl:apply-templates select="Actors/Actor"/>
    <!-- Use cases details -->
    <xsl:apply-templates select="UseCases/UseCase"/>
    <newpage/>
    <!-- Recurse package items -->
    <xsl:apply-templates select="Packages/Package"/>
    <!-- Glossary -->
    <xsl:apply-templates select="Glossary"/>
    <newpage/>
    <!-- Stakeholders -->
    <xsl:apply-templates select="Stakeholders"/>    
    <!-- Summary -->
    <!-- Summary removed in version 0.9.3 -->
    <!--
    <newpage/>
    <xsl:call-template name="MakeSummary"/>
    -->
  </xsl:template>
  
  <xsl:template match="Package">
	<!-- Common attributes -->
    <xsl:call-template name="CommonAttributes">
      <xsl:with-param name="node" select="."/>
      <xsl:with-param name="elementType" select="$package"/>
    </xsl:call-template>
    <paragraph xsl:use-attribute-sets="ParaSep"/>
    <!-- Actors list -->
    <xsl:if test="Actors/*">
      <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="text" select="$actors"/>
        </xsl:call-template>
        <xsl:for-each select="Actors/Actor">
          <xsl:call-template name="IntLinkRow">
            <xsl:with-param name="text" select="concat(@Name,' (',@Prefix,@ID,')')"/>
            <xsl:with-param name="link" select="@UniqueID"/>
          </xsl:call-template>
        </xsl:for-each>
      </table>
      <paragraph xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <!-- Use cases list -->
    <xsl:if test="UseCases/*">
      <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="text" select="$useCases"/>
        </xsl:call-template>
        <xsl:for-each select="UseCases/UseCase">
          <xsl:call-template name="IntLinkRow">
            <xsl:with-param name="text" select="concat(@Name,' (',@Prefix,@ID,')')"/>
            <xsl:with-param name="link" select="@UniqueID"/>
        </xsl:call-template>
        </xsl:for-each>
      </table>
      <paragraph xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <!-- Packages list -->
    <xsl:if test="Packages/*">
      <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="text" select="$packages"/>
        </xsl:call-template>
        <xsl:for-each select="Packages/Package">
          <xsl:call-template name="IntLinkRow">
            <xsl:with-param name="text" select="concat(@Name,' (',@Prefix,@ID,')')"/>
            <xsl:with-param name="link" select="@UniqueID"/>
        </xsl:call-template>
        </xsl:for-each>
      </table>
      <paragraph xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <!-- Requirements details -->
    <xsl:if test="Requirements/*">
      <xsl:apply-templates select="Requirements"/>
      <newpage />
    </xsl:if>
    <!-- Actors details -->
    <xsl:apply-templates select="Actors/Actor"/>
    <!-- Use cases details -->
    <xsl:apply-templates select="UseCases/UseCase"/>
    <newpage/>
    <xsl:apply-templates select="Packages/Package"/>
  </xsl:template>
  <!-- Actor details -->
  <xsl:template match="Actor">
		<!-- Common attributes -->
    <xsl:call-template name="CommonAttributes">
      <xsl:with-param name="node" select="."/>
      <xsl:with-param name="elementType" select="$actor"/>
    </xsl:call-template>
    <paragraph xsl:use-attribute-sets="ParaSep"/>
    <!-- Actor specific -->
    <xsl:if test="Goals/*">
      <xsl:apply-templates select="Goals"/>
      <paragraph xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>    
  </xsl:template>
  <!-- 'Actor' goals -->
  <xsl:template match="Goals">
    <table columns="2" width="100%" cellpadding="2.0" widths="20.0;80.0" cellsfitpage="true">
      <xsl:call-template name="HeaderRow">
        <xsl:with-param name="colspan" select="2"/>
        <xsl:with-param name="text" select="$goals"/>
      </xsl:call-template>
      <xsl:for-each select="Goal">
        <row>
        <cell xsl:use-attribute-sets="ElementTextCell">
            <paragraph xsl:use-attribute-sets="ElementText">
              <xsl:value-of select="@Name"/>
            </paragraph>
        </cell>
        <cell xsl:use-attribute-sets="ElementTextCell">
            <paragraph xsl:use-attribute-sets="ElementText">
            <xsl:call-template name="MatchLink">
                <xsl:with-param name="text" select="Description"/>
            </xsl:call-template>
            </paragraph>
        </cell>
        </row>
      </xsl:for-each>
    </table>
  </xsl:template>  
  <!-- Use case details -->
  <xsl:template match="UseCase">
		<!-- Common attributes -->
    <xsl:call-template name="CommonAttributes">
      <xsl:with-param name="node" select="."/>
      <xsl:with-param name="elementType" select="$useCase"/>
    </xsl:call-template>
    <paragraph xsl:use-attribute-sets="ParaSep"/>
    <!-- Use case specific -->
    <xsl:call-template name="General"/>
    <paragraph xsl:use-attribute-sets="ParaSep"/>
    <xsl:call-template name="Details"/>
    <xsl:if test="OpenIssues/*">
      <xsl:apply-templates select="OpenIssues"/>
      <paragraph xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <xsl:if test="Steps/*">
      <xsl:apply-templates select="Steps"/>
      <paragraph xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <xsl:if test="Prose/text() != ''">
      <xsl:apply-templates select="Prose"/>
      <paragraph xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <xsl:if test="HistoryItems/*">
      <xsl:apply-templates select="HistoryItems"/>
      <paragraph xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
  </xsl:template>
  <!-- 'Use case' general -->
  <xsl:template name="General">
    <xsl:if test="Preconditions/text() != ''">
      <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="text" select="$preconditions"/>
        </xsl:call-template>
        <row>
          <cell xsl:use-attribute-sets="ElementTextCell">
            <paragraph xsl:use-attribute-sets="ElementText">
              <xsl:call-template name="MatchLink">
                <xsl:with-param name="text" select="Preconditions/text()"/>
              </xsl:call-template>
            </paragraph>
          </cell>
        </row>
      </table>
      <paragraph xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <xsl:if test="Postconditions/text() != ''">
      <paragraph spacingafter="12.0"/>
      <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="text" select="$postconditions"/>
        </xsl:call-template>
        <row>
          <cell xsl:use-attribute-sets="ElementTextCell">
            <paragraph xsl:use-attribute-sets="ElementText">
              <xsl:call-template name="MatchLink">
                <xsl:with-param name="text" select="Postconditions/text()"/>
              </xsl:call-template>
            </paragraph>
          </cell>
        </row>
      </table>
      <paragraph xsl:use-attribute-sets="ParaSep"/>
    </xsl:if>
    <xsl:apply-templates select="Trigger"/>
    <xsl:if test="ActiveActors/*">
      <xsl:apply-templates select="ActiveActors"/>
    </xsl:if>
  </xsl:template>
  <!-- 'Use case' trigger -->
  <xsl:template match="Trigger">
    <paragraph spacingafter="12.0"/>
    <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
      <xsl:call-template name="HeaderRow">
        <xsl:with-param name="text" select="$triggerEvent"/>
      </xsl:call-template>
      <xsl:variable name="target" select="EventType/text()"/>
      <xsl:variable name="value" select="$eventTypeNodeSet[@EnumName = $target]"/>
      <row>
        <cell xsl:use-attribute-sets="ElementTextCell">
          <paragraph xsl:use-attribute-sets="ElementText">
            <xsl:value-of select="$value"/>
          </paragraph>
        </cell>
      </row>
    </table>
    <xsl:if test="Description/text() != ''">
      <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="text" select="$triggerDescription"/>
        </xsl:call-template>
        <row>
          <cell xsl:use-attribute-sets="ElementTextCell">
            <paragraph xsl:use-attribute-sets="ElementText">
              <xsl:call-template name="MatchLink">
                <xsl:with-param name="text" select="Description/text()"/>
              </xsl:call-template>
            </paragraph>
          </cell>
        </row>
      </table>
    </xsl:if>
    <paragraph xsl:use-attribute-sets="ParaSep"/>
  </xsl:template> 
  <!-- 'Use case' details -->
  <xsl:template name="Details">
    <table columns="2" width="100%" cellpadding="2.0" widths="33.33333;66.66666" cellsfitpage="true">
      <xsl:call-template name="HeaderRow">
        <xsl:with-param name="colspan" select="2"/>
        <xsl:with-param name="text" select="$details"/>
      </xsl:call-template>
      <xsl:apply-templates select="Priority"/>
      <xsl:apply-templates select="Level"/>
      <xsl:apply-templates select="Complexity"/>
      <xsl:apply-templates select="Status"/>
      <xsl:apply-templates select="Implementation"/>
      <xsl:if test="AssignedTo/text() != ''">
        <xsl:apply-templates select="AssignedTo"/>
      </xsl:if>
      <xsl:if test="Release/text() != ''">
        <xsl:apply-templates select="Release"/>
      </xsl:if>
    </table>
    <paragraph xsl:use-attribute-sets="ParaSep"/>
  </xsl:template>
  <!-- 'Use case' priority -->
  <xsl:template match="Priority">
    <row>
      <cell xsl:use-attribute-sets="ElementTextCell">
        <paragraph xsl:use-attribute-sets="ElementText">
          <xsl:value-of select="$priority"/>
        </paragraph>
      </cell>
      <cell xsl:use-attribute-sets="ElementTextCell">
        <paragraph xsl:use-attribute-sets="ElementText">
          <xsl:value-of select="text()"/>
        </paragraph>
      </cell>
    </row>
  </xsl:template>
  <!-- 'Use case' level -->
  <xsl:template match="Level">
    <xsl:variable name="target" select="text()"/>
    <xsl:variable name="value" select="$levelNodeSet[@EnumName = $target]"/>
    <row>
      <cell xsl:use-attribute-sets="ElementTextCell">
        <paragraph xsl:use-attribute-sets="ElementText">
          <xsl:value-of select="$level"/>
        </paragraph>
      </cell>
      <cell xsl:use-attribute-sets="ElementTextCell">
        <paragraph xsl:use-attribute-sets="ElementText">
          <xsl:value-of select="$value"/>
        </paragraph>
      </cell>
    </row>
  </xsl:template>
  <!-- 'Use case' complexity -->
  <xsl:template match="Complexity">
    <xsl:variable name="target" select="text()"/>
    <xsl:variable name="value" select="$complexityNodeSet[@EnumName = $target]"/>
    <row>
      <cell xsl:use-attribute-sets="ElementTextCell">
        <paragraph xsl:use-attribute-sets="ElementText">
          <xsl:value-of select="$complexity"/>
        </paragraph>
      </cell>
      <cell xsl:use-attribute-sets="ElementTextCell">
        <paragraph xsl:use-attribute-sets="ElementText">
          <xsl:value-of select="$value"/>
        </paragraph>
      </cell>
    </row>
  </xsl:template>
  <!-- 'Use case' status -->
  <xsl:template match="Status">
    <xsl:variable name="target" select="text()"/>
    <xsl:variable name="value" select="$statusNodeSet[@EnumName = $target]"/>
    <row>
      <cell xsl:use-attribute-sets="ElementTextCell">
        <paragraph xsl:use-attribute-sets="ElementText">
          <xsl:value-of select="$status"/>
        </paragraph>
      </cell>
      <cell xsl:use-attribute-sets="ElementTextCell">
        <paragraph xsl:use-attribute-sets="ElementText">
          <xsl:value-of select="$value"/>
        </paragraph>
      </cell>
    </row>
  </xsl:template>
  <!-- 'Use case' implementation -->
  <xsl:template match="Implementation">
    <xsl:variable name="target" select="text()"/>
    <xsl:variable name="value" select="$implementationNodeSet[@EnumName = $target]"/>
    <row>
      <cell xsl:use-attribute-sets="ElementTextCell">
        <paragraph xsl:use-attribute-sets="ElementText">
          <xsl:value-of select="$implementation"/>
        </paragraph>
      </cell>
      <cell xsl:use-attribute-sets="ElementTextCell">
        <paragraph xsl:use-attribute-sets="ElementText">
          <xsl:value-of select="$value"/>
        </paragraph>
      </cell>
    </row>
  </xsl:template>
  <!-- 'Use case' assigned to -->
  <xsl:template match="AssignedTo">
    <row>
      <cell xsl:use-attribute-sets="ElementTextCell">
        <paragraph xsl:use-attribute-sets="ElementText">
          <xsl:value-of select="$assignedTo"/>
        </paragraph>
      </cell>
      <cell xsl:use-attribute-sets="ElementTextCell">
        <paragraph xsl:use-attribute-sets="ElementText">
          <xsl:value-of select="text()"/>
        </paragraph>
      </cell>
    </row>
  </xsl:template>
  <!-- 'Use case' release -->
  <xsl:template match="Release">
    <row>
      <cell xsl:use-attribute-sets="ElementTextCell">
        <paragraph xsl:use-attribute-sets="ElementText">
          <xsl:value-of select="$release"/>
        </paragraph>
      </cell>
      <cell xsl:use-attribute-sets="ElementTextCell">
        <paragraph xsl:use-attribute-sets="ElementText">
          <xsl:value-of select="text()"/>
        </paragraph>
      </cell>
    </row>
  </xsl:template>
  <!-- 'Use case' active actors -->
  <xsl:template match="ActiveActors">
      <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
      <xsl:call-template name="HeaderRow">
        <xsl:with-param name="text" select="$activeActors"/>
      </xsl:call-template>
      <xsl:for-each select="ActiveActor">
        <xsl:variable name="target" select="ActorUniqueID/text()"/>
        <xsl:choose>
        <xsl:when test="IsPrimary/text() = 'True'">
            <xsl:call-template name="IntLinkRow">
            <xsl:with-param name="text" select="concat(//Actor[@UniqueID = $target]/@Name,' (',$primary,')')"/>
            <xsl:with-param name="link" select="$target"/>
            </xsl:call-template>
        </xsl:when>
        <xsl:otherwise>
            <xsl:call-template name="IntLinkRow">
            <xsl:with-param name="text" select="//Actor[@UniqueID = $target]/@Name"/>
            <xsl:with-param name="link" select="$target"/>
            </xsl:call-template>
        </xsl:otherwise>
        </xsl:choose>
      </xsl:for-each>
    </table>
  </xsl:template>
  <!-- 'Use case' open issues -->
  <xsl:template match="OpenIssues">
    <table columns="2" width="100%" cellpadding="2.0" widths="20.0;80.0" cellsfitpage="true">
      <xsl:call-template name="HeaderRow">
        <xsl:with-param name="colspan" select="2"/>
        <xsl:with-param name="text" select="$openIssues"/>
      </xsl:call-template>
      <xsl:for-each select="OpenIssue">
        <row>
        <cell xsl:use-attribute-sets="ElementTextCell">
            <paragraph xsl:use-attribute-sets="ElementText">
              <xsl:value-of select="@Name"/>
            </paragraph>
        </cell>
        <cell xsl:use-attribute-sets="ElementTextCell">
            <paragraph xsl:use-attribute-sets="ElementText">
            <xsl:call-template name="MatchLink">
                <xsl:with-param name="text" select="Description"/>
            </xsl:call-template>
            </paragraph>
        </cell>
        </row>
      </xsl:for-each>
    </table>
  </xsl:template>
  <!-- 'Use case' flow of events -->
  <xsl:template match="Steps">
    <table columns="2" width="100%" cellpadding="2.0" widths="20.0;80.0" cellsfitpage="true">
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="colspan" select="2"/>
          <xsl:with-param name="text" select="$flowOfEvents"/>
        </xsl:call-template>
        <xsl:for-each select="Step">
          <row>
            <cell xsl:use-attribute-sets="ElementTextCell">
              <paragraph xsl:use-attribute-sets="ElementText">
                <xsl:value-of select="@Name"/>
              </paragraph>
            </cell>
            <cell xsl:use-attribute-sets="ElementTextCell">
              <paragraph xsl:use-attribute-sets="ElementText">
                <xsl:call-template name="MatchLink">
                  <xsl:with-param name="text" select="Description"/>
                </xsl:call-template>
              </paragraph>
            </cell>
          </row>
        </xsl:for-each>
    </table>
  </xsl:template>
  <!-- 'Use case' prose -->
  <xsl:template match="Prose">
    <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="text" select="$prose"/>
        </xsl:call-template>
        <row>
          <cell xsl:use-attribute-sets="ElementTextCell">
            <paragraph xsl:use-attribute-sets="ElementText">
              <xsl:call-template name="MatchLink">
                <xsl:with-param name="text" select="text()"/>
              </xsl:call-template>
            </paragraph>
          </cell>
        </row>
    </table>
  </xsl:template>
  <!-- 'Use case' requirements -->
  <xsl:template match="Requirements">
    <newpage/>
    <paragraph xsl:use-attribute-sets="ParaTitle">
      <xsl:value-of select="$requirements"/> (<xsl:value-of select="../@Name"/>)
    </paragraph>
    <xsl:for-each select="Requirement">
      <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="text" select="@Name"/>
        </xsl:call-template>
        <row>
          <cell xsl:use-attribute-sets="ElementTextCell">
            <paragraph xsl:use-attribute-sets="ElementText">
              <xsl:call-template name="MatchLink">
                <xsl:with-param name="text" select="Description"/>
              </xsl:call-template>
            </paragraph>
          </cell>
        </row>
      </table>
      <xsl:if test="Proponents/*">
        <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
          <xsl:call-template name="HeaderRow">
            <xsl:with-param name="text" select="$proposedBy"/>
          </xsl:call-template>
          <xsl:for-each select="Proponents/ReferencedObject">
            <xsl:variable name="target" select="UniqueID"/>
            <xsl:call-template name="IntLinkRow">
              <xsl:with-param name="text" select="//*[@UniqueID = $target]/@Name"/>
              <xsl:with-param name="link" select="$target"/>
            </xsl:call-template>
          </xsl:for-each>
        </table>
      </xsl:if>
      <xsl:if test="Beneficiaries/*">
        <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
          <xsl:call-template name="HeaderRow">
            <xsl:with-param name="text" select="$benefitTo"/>
          </xsl:call-template>
          <xsl:for-each select="Beneficiaries/ReferencedObject">
            <xsl:variable name="target" select="UniqueID"/>
            <xsl:call-template name="IntLinkRow">
              <xsl:with-param name="text" select="//*[@UniqueID = $target]/@Name"/>
              <xsl:with-param name="link" select="$target"/>
            </xsl:call-template>
          </xsl:for-each>
        </table>
      </xsl:if>
      <xsl:apply-templates select="Category" />
      <xsl:apply-templates select="Importance" />
      <xsl:apply-templates select="Status" />
      <xsl:apply-templates select="AcceptanceStatus" />
      <xsl:if test="MappedUseCases/*">
        <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
          <xsl:call-template name="HeaderRow">
            <xsl:with-param name="text" select="$mappedUCs"/>
          </xsl:call-template>
          <xsl:for-each select="MappedUseCases/ReferencedObject">
            <xsl:variable name="target" select="UniqueID"/>
            <xsl:call-template name="IntLinkRow">
              <xsl:with-param name="text" select="//*[@UniqueID = $target]/@Name"/>
              <xsl:with-param name="link" select="$target"/>
            </xsl:call-template>
          </xsl:for-each>
        </table>
      </xsl:if>
      <paragraph xsl:use-attribute-sets="ParaSep"/>
    </xsl:for-each>
  </xsl:template>
  <!-- 'Requirement' category -->
  <xsl:template match="Category">
    <xsl:variable name="target" select="text()"/>
    <xsl:variable name="value" select="$categoryNodeSet[@EnumName = $target]"/>
    <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
      <xsl:call-template name="HeaderRow">
        <xsl:with-param name="text" select="$category"/>
      </xsl:call-template>
      <row>
        <cell xsl:use-attribute-sets="ElementTextCell">
          <paragraph xsl:use-attribute-sets="ElementText">
            <xsl:value-of select="$value"/>
          </paragraph>
        </cell>
      </row>
    </table>
  </xsl:template>
  <!-- 'Requirement' importance -->
  <xsl:template match="Importance">
    <xsl:variable name="target" select="text()"/>
    <xsl:variable name="value" select="$importanceNodeSet[@EnumName = $target]"/>
    <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
      <xsl:call-template name="HeaderRow">
        <xsl:with-param name="text" select="$importance"/>
      </xsl:call-template>
      <row>
        <cell xsl:use-attribute-sets="ElementTextCell">
          <paragraph xsl:use-attribute-sets="ElementText">
            <xsl:value-of select="$value"/>
          </paragraph>
        </cell>
      </row>
    </table>
  </xsl:template>
  <!-- 'Requirement' status -->
  <xsl:template match="Requirement/Status">
    <xsl:variable name="target" select="text()"/>
    <xsl:variable name="value" select="$statusNodeSet[@EnumName = $target]"/>
    <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
      <xsl:call-template name="HeaderRow">
        <xsl:with-param name="text" select="$status"/>
      </xsl:call-template>
      <row>
        <cell xsl:use-attribute-sets="ElementTextCell">
          <paragraph xsl:use-attribute-sets="ElementText">
            <xsl:value-of select="$value"/>
          </paragraph>
        </cell>
      </row>
    </table>
  </xsl:template>
  <!-- 'Requirement' acceptance -->
  <xsl:template match="AcceptanceStatus">
    <xsl:variable name="target" select="text()"/>
    <xsl:variable name="value" select="$acceptanceNodeSet[@EnumName = $target]"/>
    <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
      <xsl:call-template name="HeaderRow">
        <xsl:with-param name="text" select="$acceptance"/>
      </xsl:call-template>
      <row>
        <cell xsl:use-attribute-sets="ElementTextCell">
          <paragraph xsl:use-attribute-sets="ElementText">
            <xsl:value-of select="$value"/>
          </paragraph>
        </cell>
      </row>
    </table>
  </xsl:template>
  <!-- 'Use case' history -->
  <xsl:template match="HistoryItems">
    <table columns="4" width="100%" cellpadding="2.0" widths="20.0;20.0;20.0;40.0" cellsfitpage="true">
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="colspan" select="4"/>
          <xsl:with-param name="text" select="$history"/>
        </xsl:call-template>
        <xsl:for-each select="HistoryItem">
          <row>
            <cell xsl:use-attribute-sets="ElementTextCell">
              <paragraph xsl:use-attribute-sets="ElementText">
                <xsl:value-of select="substring-before(DateValue,' ')"/>
              </paragraph>
            </cell>
            <cell xsl:use-attribute-sets="ElementTextCell">
              <xsl:variable name="typeTarget" select="Type/text()"/>
              <xsl:variable name="typeValue" select="$historyTypeNodeSet[@EnumName = $typeTarget]"/>
              <paragraph xsl:use-attribute-sets="ElementText">
                <xsl:value-of select="$typeValue"/>
              </paragraph>
            </cell>
            <xsl:variable name="actionTarget" select="Action/text()"/>
            <xsl:choose>
              <xsl:when test="Type/text() = 'Status'">
                <cell xsl:use-attribute-sets="ElementTextCell">
                  <xsl:variable name="statusValue" select="$statusNodeSet[@ListIndex = $actionTarget]"/>
                  <paragraph xsl:use-attribute-sets="ElementText">
                    <xsl:value-of select="$statusValue"/>
                  </paragraph>
                </cell>
              </xsl:when>
              <xsl:otherwise>
                <cell xsl:use-attribute-sets="ElementTextCell">
                  <xsl:variable name="implValue" select="$implementationNodeSet[@ListIndex = $actionTarget]"/>
                  <paragraph xsl:use-attribute-sets="ElementText">
                    <xsl:value-of select="$implValue"/>
                  </paragraph>
                </cell>
              </xsl:otherwise>
            </xsl:choose>
            <cell xsl:use-attribute-sets="ElementTextCell">
              <paragraph xsl:use-attribute-sets="ElementText">
                <xsl:value-of select="Notes"/>
              </paragraph>
            </cell>
          </row>
        </xsl:for-each>
    </table>
  </xsl:template>
  <!-- General common attributes -->
  <xsl:template name="CommonAttributes">
    <xsl:param name="node"/>
    <xsl:param name="elementType"/>
    <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
        <xsl:call-template name="HeaderRow">
          <xsl:with-param name="text" select="$elementType"/>
        </xsl:call-template>
        <xsl:call-template name="NameRow">
          <xsl:with-param name="text" select="concat($node/@Name,' (',$node/@Prefix,$node/@ID,')')"/>
          <xsl:with-param name="uniqueID" select="$node/@UniqueID"/>
        </xsl:call-template>
    </table>
    <paragraph spacingafter="12.0"/>
    <xsl:if test="$node/Attributes/Description/text() != ''">
      <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
          <xsl:call-template name="HeaderRow">
            <xsl:with-param name="text" select="$description"/>
          </xsl:call-template>
          <row>
            <cell xsl:use-attribute-sets="ElementTextCell">
              <paragraph xsl:use-attribute-sets="ElementText">
                <xsl:call-template name="MatchLink">
                  <xsl:with-param name="text" select="$node/Attributes/Description/text()"/>
                </xsl:call-template>
              </paragraph>
            </cell>
          </row>                 
      </table>
      <paragraph spacingafter="12.0"/>
    </xsl:if>
    <xsl:if test="$node/Attributes/Notes/text() != ''">
      <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
          <xsl:call-template name="HeaderRow">
            <xsl:with-param name="text" select="$notes"/>
          </xsl:call-template>
          <row>
            <cell xsl:use-attribute-sets="ElementTextCell">
              <paragraph xsl:use-attribute-sets="ElementText">
                <xsl:call-template name="MatchLink">
                  <xsl:with-param name="text" select="$node/Attributes/Notes/text()"/>
                </xsl:call-template>
              </paragraph>
            </cell>
          </row>
      </table>
      <paragraph spacingafter="12.0"/>
    </xsl:if>
    <xsl:if test="$node/Attributes/RelatedDocuments/*">
      <table columns="1" width="100%" cellpadding="2.0" cellsfitpage="true">
          <xsl:call-template name="HeaderRow">
            <xsl:with-param name="text" select="$relatedDocs"/>
          </xsl:call-template>
          <xsl:for-each select="$node/Attributes/RelatedDocuments/RelatedDocument">
            <xsl:call-template name="ExtLinkRow">
              <xsl:with-param name="text" select="FileName/text()"/>
              <xsl:with-param name="link" select="concat('file:///',translate(FileName/text(),'\\','/'))"/>
            </xsl:call-template>
          </xsl:for-each>
      </table>
    </xsl:if>
  </xsl:template>
  <xsl:template name="HeaderRow">
    <xsl:param name="colspan" select="1"/>
    <xsl:param name="text"/>
    <row>
      <xsl:element name="cell" use-attribute-sets="ElementTitleCell">
        <xsl:attribute name="colspan">
          <xsl:value-of select="$colspan"/>
        </xsl:attribute>
        <paragraph xsl:use-attribute-sets="ElementTitle">
          <xsl:value-of select="$text"/>
        </paragraph>
      </xsl:element>
    </row>
  </xsl:template>
  <xsl:template name="NameRow">
    <xsl:param name="text"/>
    <xsl:param name="uniqueID"/>
    <row>
      <cell xsl:use-attribute-sets="ElementNameCell">
        <xsl:choose>
          <xsl:when test="$outputType = 'withLink'">
		    <xsl:element name="anchor" use-attribute-sets="ElementName">
		      <xsl:attribute name="name">
			    <xsl:value-of select="$uniqueID"/>
			  </xsl:attribute>
			  <paragraph>
    		    <xsl:element name="chunk">
	   	          <xsl:attribute name="generictag">
		            <xsl:value-of select="$text"/>
		          </xsl:attribute>
		          <xsl:value-of select="$text"/>
		        </xsl:element>
		      </paragraph>
            </xsl:element>  
          </xsl:when>  
          <xsl:otherwise>
            <paragraph xsl:use-attribute-sets="ElementName">
              <xsl:value-of select="$text"/>
		    </paragraph>
          </xsl:otherwise>
        </xsl:choose>
      </cell>
    </row>
  </xsl:template>
  <xsl:template name="TextRow">
    <xsl:param name="text"/>
    <row>
      <cell xsl:use-attribute-sets="ElementTextCell">
        <paragraph xsl:use-attribute-sets="ElementText">
          <xsl:value-of select="$text"/>
        </paragraph>
      </cell>
    </row>
  </xsl:template>
  <xsl:template name="ExtLinkRow">
    <xsl:param name="text"/>
    <xsl:param name="link"/>
    <row>
      <cell xsl:use-attribute-sets="ElementLinkCell">
        <paragraph>
          <xsl:element name="anchor" use-attribute-sets="ElementLink">
            <xsl:attribute name="reference">
              <xsl:value-of select="$link"/>
            </xsl:attribute>
            <xsl:value-of select="$text"/>
          </xsl:element>
        </paragraph>
      </cell>
    </row>
  </xsl:template>
  <xsl:template name="IntLinkRow">
    <xsl:param name="text"/>
    <xsl:param name="link"/>
    <row>
      <cell xsl:use-attribute-sets="ElementLinkCell">
        <xsl:choose>
          <xsl:when test="$outputType = 'withLink'">
            <paragraph>
              <xsl:element name="anchor" use-attribute-sets="ElementLink">
                <xsl:attribute name="reference">
                  <xsl:value-of select="concat('#',$link)"/>
                </xsl:attribute>
                <xsl:value-of select="$text"/>
              </xsl:element>
            </paragraph>
          </xsl:when>
          <xsl:otherwise>
            <paragraph xsl:use-attribute-sets="ElementLink">
              <xsl:value-of select="$text"/>
            </paragraph>
          </xsl:otherwise>
        </xsl:choose>
      </cell>
    </row>
  </xsl:template>
  <xsl:template name="MatchLink">
    <xsl:param name="text"/>
    <xsl:choose>
      <xsl:when test="contains($text,'&#34;') and string-length($text) > 1">
        <xsl:variable name="start" select="substring-after($text, '&#34;')" />
        <xsl:variable name="end" select="substring-before($start, '&#34;')" />
        <xsl:choose>
          <xsl:when test="$end">
            <xsl:if test="$start">
			  <xsl:call-template name="lf2nl"> 
                <xsl:with-param name="text" select="substring-before($text,$end)"/>
              </xsl:call-template>
            </xsl:if>
            <xsl:choose>
              <xsl:when test="//Glossary/*[@Name = $end]">
                <xsl:call-template name="MakeAnchorLink">
                  <xsl:with-param name="glossary_uid" select="//Glossary/@UniqueID"/>
                  <xsl:with-param name="text" select="$end"/>
                  <xsl:with-param name="node" select="//Glossary/*[@Name = $end]"/>
                </xsl:call-template>
                <xsl:call-template name="MatchLink">
                  <xsl:with-param name="text" select="substring-after($text,concat($end,'&#34;'))" />
                </xsl:call-template>
              </xsl:when>
              <xsl:otherwise>
                <xsl:choose>
                  <xsl:when test="//*[@Path = $end]">
                    <xsl:call-template name="MakePathLink">
                      <xsl:with-param name="text" select="$end"/>
                      <xsl:with-param name="node" select="//*[@Path = $end]"/>
                    </xsl:call-template>
                    <xsl:call-template name="MatchLink">
                      <xsl:with-param name="text" select="substring-after($text,concat($end,'&#34;'))" />
                    </xsl:call-template>
                  </xsl:when>
                  <xsl:when test="//*[@Name = $end]">
                    <xsl:call-template name="MakeNameLink">
                      <xsl:with-param name="text" select="$end"/>
                      <xsl:with-param name="node" select="//*[@Name = $end]"/>
                    </xsl:call-template>
                    <xsl:call-template name="MatchLink">
                      <xsl:with-param name="text" select="substring-after($text,concat($end,'&#34;'))" />
                    </xsl:call-template>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:call-template name="lf2nl">
                      <xsl:with-param name="text" select="$end"/>
                    </xsl:call-template>
                    <xsl:call-template name="MatchLink">
                      <xsl:with-param name="text" select="substring-after($text,$end)"/>
                    </xsl:call-template>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:when>
          <xsl:otherwise>
            <xsl:call-template name="MatchLink">
              <xsl:with-param name="text" select="substring-after($text,$start)"/>
            </xsl:call-template>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:when>
      <xsl:otherwise>
        <xsl:call-template name="lf2nl">
          <xsl:with-param name="text" select="$text"/>
        </xsl:call-template>  
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="MakeNameLink">
    <xsl:param name="text"/>
    <xsl:param name="node"/>
    <xsl:choose>
      <xsl:when test="$outputType = 'withLink'">
        <xsl:element name="anchor" use-attribute-sets="ElementLink">
          <xsl:attribute name="reference">
            <xsl:value-of select="concat('#',$node/@UniqueID)"/>
          </xsl:attribute>
          <xsl:value-of select="$text"/>
        </xsl:element>
        <chunk>
          <xsl:value-of select="'&#34;'" />
        </chunk>
      </xsl:when>
      <xsl:otherwise>
        <chunk xsl:use-attribute-sets="ElementLink">
          <xsl:value-of select="$text"/>
        </chunk>
        <chunk>
          <xsl:value-of select="'&#34;'" />
        </chunk>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="MakePathLink">
    <xsl:param name="text"/>
    <xsl:param name="node"/>
    <xsl:choose>
      <xsl:when test="$outputType = 'withLink'">
        <xsl:element name="anchor" use-attribute-sets="ElementLink">
          <xsl:attribute name="reference">
            <xsl:value-of select="concat('#',$node/@UniqueID)"/>
          </xsl:attribute>
          <xsl:value-of select="$text"/>
        </xsl:element>
        <chunk>
          <xsl:value-of select="'&#34;'" />
        </chunk>
      </xsl:when>
      <xsl:otherwise>
        <chunk xsl:use-attribute-sets="ElementLink">
          <xsl:value-of select="$text"/>
        </chunk>
        <chunk>
          <xsl:value-of select="'&#34;'" />
        </chunk>
      </xsl:otherwise>
    </xsl:choose>  
  </xsl:template>
  <xsl:template name="MakeAnchorLink">
    <xsl:param name="glossary_uid"/>
    <xsl:param name="text"/>
    <xsl:param name="node"/>
    <xsl:choose>
      <xsl:when test="$outputType = 'withLink'">
        <xsl:element name="anchor" use-attribute-sets="ElementLink">
          <xsl:attribute name="reference">
            <xsl:value-of select="concat('#',$node/@UniqueID)"/>
          </xsl:attribute>
          <xsl:value-of select="$text"/>
        </xsl:element>
        <chunk>
          <xsl:value-of select="'&#34;'" />
        </chunk>
      </xsl:when>
      <xsl:otherwise>
        <chunk xsl:use-attribute-sets="ElementLink">
          <xsl:value-of select="$text"/>
        </chunk>
        <chunk>
          <xsl:value-of select="'&#34;'" />
        </chunk>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="lf2nl">
    <xsl:param name="text" />
    <xsl:choose>
		<xsl:when test="contains($text, '&#10;')">
			<xsl:variable name="head" select="substring-before($text, '&#10;')" />
			<xsl:variable name="tail" select="substring-after($text, '&#10;')" />
			<xsl:value-of select="$head" />
			<newline/>
			<xsl:if test="$tail">
				<xsl:call-template name="lf2nl">
					<xsl:with-param name="text" select="$tail" />
				</xsl:call-template>
			</xsl:if>
		</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="$text" />
		</xsl:otherwise>
	</xsl:choose>
  </xsl:template>  
</xsl:stylesheet>
