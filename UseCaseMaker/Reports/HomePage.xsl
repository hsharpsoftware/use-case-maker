<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" />
	
	<xsl:param name="version" />
  <xsl:param name="model" />
  <xsl:param name="author" />
  <xsl:param name="company" />
  <xsl:param name="creationDate" />
  <xsl:param name="exportPrintDate" />
  <xsl:param name="now" />
  <xsl:param name="release" />

	<xsl:template match="/">
    <html>
      <head>
        <title></title>
        <link rel="stylesheet" type="text/css" href="ucm.css" />
      </head>
      <body>
      <xsl:apply-templates/>
      </body>
    </html>      
  </xsl:template>

  <xsl:template match="Model">
    <p align="center">
      Use Case Maker <xsl:value-of select="$version" />
    </p>
    <hr></hr>
    <br></br>
    <p align="center">
      <xsl:value-of select="$model"/>
    </p>    
    <p class="Title" align="center">
      <xsl:value-of select="@Name"/>
    </p>
    <br></br>
    <xsl:if test="@Author != ''">
      <p align="center">
        <xsl:value-of select="$author"/>
      </p>
      <p class="SubTitle" align="center">
        <xsl:value-of select="@Author"/>
      </p>
      <br></br>
    </xsl:if>
    <xsl:if test="@Company != ''">
      <p align="center">
        <xsl:value-of select="$company"/>
      </p>
      <p class="SubTitle" align="center">
        <xsl:value-of select="@Company"/>
      </p>
      <br></br>
    </xsl:if>
    <xsl:if test="@Release != ''">
      <p align="center">
        <xsl:value-of select="$release"/>
        <xsl:value-of select="concat(': ',@Release)"/>
      </p>
      <br></br>
    </xsl:if>
    <p align="center">
      <xsl:value-of select="$creationDate"/>:
      <xsl:value-of select="substring-before(@CreationDateValue,' ')"/>
    </p>
    <p align="center">
      <xsl:value-of select="$exportPrintDate"/>:
      <xsl:value-of select="substring-before($now,' ')"/>
    </p>
    <br></br>
    <hr></hr>
  </xsl:template>
</xsl:stylesheet>
  